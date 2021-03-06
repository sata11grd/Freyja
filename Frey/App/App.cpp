/*
 *   Copyright(C) 2011-2018 Intel Corporation All Rights Reserved.
 *
 *   The source code, information  and  material ("Material") contained herein is
 *   owned  by Intel Corporation or its suppliers or licensors, and title to such
 *   Material remains  with Intel Corporation  or its suppliers or licensors. The
 *   Material  contains proprietary information  of  Intel or  its  suppliers and
 *   licensors. The  Material is protected by worldwide copyright laws and treaty
 *   provisions. No  part  of  the  Material  may  be  used,  copied, reproduced,
 *   modified, published, uploaded, posted, transmitted, distributed or disclosed
 *   in any way  without Intel's  prior  express written  permission. No  license
 *   under  any patent, copyright  or  other intellectual property rights  in the
 *   Material  is  granted  to  or  conferred  upon  you,  either  expressly,  by
 *   implication, inducement,  estoppel or  otherwise.  Any  license  under  such
 *   intellectual  property  rights must  be express  and  approved  by  Intel in
 *   writing.
 *
 *   *Third Party trademarks are the property of their respective owners.
 *
 *   Unless otherwise  agreed  by Intel  in writing, you may not remove  or alter
 *   this  notice or  any other notice embedded  in Materials by Intel or Intel's
 *   suppliers or licensors in any way.
 *
 */

#include <stdio.h>
#include <string.h>
#include <assert.h>

#ifdef _MSC_VER
# include <Shlobj.h>
#else
# include <unistd.h>
# include <pwd.h>
# define MAX_PATH FILENAME_MAX
#endif

#include "sgx_urts.h"
#include "App.h"
#include "Enclave_u.h"

#pragma region SGX Base Funcs
 /* Global EID shared by multiple threads */
sgx_enclave_id_t global_eid = 0;

typedef struct _sgx_errlist_t {
	sgx_status_t err;
	const char *msg;
	const char *sug; /* Suggestion */
} sgx_errlist_t;

/* Error code returned by sgx_create_enclave */
static sgx_errlist_t sgx_errlist[] = {
	{
		SGX_ERROR_UNEXPECTED,
		"Unexpected error occurred.",
		NULL
	},
	{
		SGX_ERROR_INVALID_PARAMETER,
		"Invalid parameter.",
		NULL
	},
	{
		SGX_ERROR_OUT_OF_MEMORY,
		"Out of memory.",
		NULL
	},
	{
		SGX_ERROR_ENCLAVE_LOST,
		"Power transition occurred.",
		"Please refer to the sample \"PowerTransition\" for details."
	},
	{
		SGX_ERROR_INVALID_ENCLAVE,
		"Invalid enclave image.",
		NULL
	},
	{
		SGX_ERROR_INVALID_ENCLAVE_ID,
		"Invalid enclave identification.",
		NULL
	},
	{
		SGX_ERROR_INVALID_SIGNATURE,
		"Invalid enclave signature.",
		NULL
	},
	{
		SGX_ERROR_OUT_OF_EPC,
		"Out of EPC memory.",
		NULL
	},
	{
		SGX_ERROR_NO_DEVICE,
		"Invalid SGX device.",
		"Please make sure SGX module is enabled in the BIOS, and install SGX driver afterwards."
	},
	{
		SGX_ERROR_MEMORY_MAP_CONFLICT,
		"Memory map conflicted.",
		NULL
	},
	{
		SGX_ERROR_INVALID_METADATA,
		"Invalid enclave metadata.",
		NULL
	},
	{
		SGX_ERROR_DEVICE_BUSY,
		"SGX device was busy.",
		NULL
	},
	{
		SGX_ERROR_INVALID_VERSION,
		"Enclave version was invalid.",
		NULL
	},
	{
		SGX_ERROR_INVALID_ATTRIBUTE,
		"Enclave was not authorized.",
		NULL
	},
	{
		SGX_ERROR_ENCLAVE_FILE_ACCESS,
		"Can't open enclave file.",
		NULL
	},
	{
		SGX_ERROR_NDEBUG_ENCLAVE,
		"The enclave is signed as product enclave, and can not be created as debuggable enclave.",
		NULL
	},
};

/* Check error conditions for loading enclave */
void print_error_message(sgx_status_t ret)
{
	size_t idx = 0;
	size_t ttl = sizeof sgx_errlist / sizeof sgx_errlist[0];

	for (idx = 0; idx < ttl; idx++) {
		if (ret == sgx_errlist[idx].err) {
			if (NULL != sgx_errlist[idx].sug)
				printf("Info: %s\n", sgx_errlist[idx].sug);
			printf("Error: %s\n", sgx_errlist[idx].msg);
			break;
		}
	}

	if (idx == ttl)
		printf("Error code is 0x%X. Please refer to the \"Intel SGX SDK Developer Reference\" for more details.\n", ret);
}

/* Initialize the enclave:
 *   Step 1: try to retrieve the launch token saved by last transaction
 *   Step 2: call sgx_create_enclave to initialize an enclave instance
 *   Step 3: save the launch token if it is updated
 */
int initialize_enclave(void)
{
	char token_path[MAX_PATH] = { '\0' };
	sgx_launch_token_t token = { 0 };
	sgx_status_t ret = SGX_ERROR_UNEXPECTED;
	int updated = 0;

	/* Step 1: try to retrieve the launch token saved by last transaction
	 *         if there is no token, then create a new one.
	 */
#ifdef _MSC_VER
	 /* try to get the token saved in CSIDL_LOCAL_APPDATA */
	if (S_OK != SHGetFolderPathA(NULL, CSIDL_LOCAL_APPDATA, NULL, 0, token_path)) {
		strncpy_s(token_path, _countof(token_path), TOKEN_FILENAME, sizeof(TOKEN_FILENAME));
	}
	else {
		strncat_s(token_path, _countof(token_path), "\\" TOKEN_FILENAME, sizeof(TOKEN_FILENAME) + 2);
	}

	/* open the token file */
	HANDLE token_handler = CreateFileA(token_path, GENERIC_READ | GENERIC_WRITE, FILE_SHARE_READ, NULL, OPEN_ALWAYS, NULL, NULL);
	if (token_handler == INVALID_HANDLE_VALUE) {
		printf("Warning: Failed to create/open the launch token file \"%s\".\n", token_path);
	}
	else {
		/* read the token from saved file */
		DWORD read_num = 0;
		ReadFile(token_handler, token, sizeof(sgx_launch_token_t), &read_num, NULL);
		if (read_num != 0 && read_num != sizeof(sgx_launch_token_t)) {
			/* if token is invalid, clear the buffer */
			memset(&token, 0x0, sizeof(sgx_launch_token_t));
			printf("Warning: Invalid launch token read from \"%s\".\n", token_path);
		}
	}
#else /* __GNUC__ */
	 /* try to get the token saved in $HOME */
	const char *home_dir = getpwuid(getuid())->pw_dir;

	if (home_dir != NULL &&
		(strlen(home_dir) + strlen("/") + sizeof(TOKEN_FILENAME) + 1) <= MAX_PATH) {
		/* compose the token path */
		strncpy(token_path, home_dir, strlen(home_dir));
		strncat(token_path, "/", strlen("/"));
		strncat(token_path, TOKEN_FILENAME, sizeof(TOKEN_FILENAME) + 1);
	}
	else {
		/* if token path is too long or $HOME is NULL */
		strncpy(token_path, TOKEN_FILENAME, sizeof(TOKEN_FILENAME));
	}

	FILE *fp = fopen(token_path, "rb");
	if (fp == NULL && (fp = fopen(token_path, "wb")) == NULL) {
		printf("Warning: Failed to create/open the launch token file \"%s\".\n", token_path);
	}

	if (fp != NULL) {
		/* read the token from saved file */
		size_t read_num = fread(token, 1, sizeof(sgx_launch_token_t), fp);
		if (read_num != 0 && read_num != sizeof(sgx_launch_token_t)) {
			/* if token is invalid, clear the buffer */
			memset(&token, 0x0, sizeof(sgx_launch_token_t));
			printf("Warning: Invalid launch token read from \"%s\".\n", token_path);
		}
	}
#endif
	/* Step 2: call sgx_create_enclave to initialize an enclave instance */
	/* Debug Support: set 2nd parameter to 1 */
	ret = sgx_create_enclave(ENCLAVE_FILENAME, SGX_DEBUG_FLAG, &token, &updated, &global_eid, NULL);
	if (ret != SGX_SUCCESS) {
		print_error_message(ret);
#ifdef _MSC_VER
		if (token_handler != INVALID_HANDLE_VALUE)
			CloseHandle(token_handler);
#else
		if (fp != NULL) fclose(fp);
#endif
		return -1;
	}

	/* Step 3: save the launch token if it is updated */
#ifdef _MSC_VER
	if (updated == FALSE || token_handler == INVALID_HANDLE_VALUE) {
		/* if the token is not updated, or file handler is invalid, do not perform saving */
		if (token_handler != INVALID_HANDLE_VALUE)
			CloseHandle(token_handler);
		return 0;
	}

	/* flush the file cache */
	FlushFileBuffers(token_handler);
	/* set access offset to the begin of the file */
	SetFilePointer(token_handler, 0, NULL, FILE_BEGIN);

	/* write back the token */
	DWORD write_num = 0;
	WriteFile(token_handler, token, sizeof(sgx_launch_token_t), &write_num, NULL);
	if (write_num != sizeof(sgx_launch_token_t))
		printf("Warning: Failed to save launch token to \"%s\".\n", token_path);
	CloseHandle(token_handler);
#else /* __GNUC__ */
	if (updated == FALSE || fp == NULL) {
		/* if the token is not updated, or file handler is invalid, do not perform saving */
		if (fp != NULL) fclose(fp);
		return 0;
	}

	/* reopen the file with write capablity */
	fp = freopen(token_path, "wb", fp);
	if (fp == NULL) return 0;
	size_t write_num = fwrite(token, 1, sizeof(sgx_launch_token_t), fp);
	if (write_num != sizeof(sgx_launch_token_t))
		printf("Warning: Failed to save launch token to \"%s\".\n", token_path);
	fclose(fp);
#endif
	return 0;
}

void ocall_print_string(const char *str)
{
	printf("%s", str);
}
#pragma endregion

#pragma region Dll Management
char* str_export(char* str, int size) {
	char* szSampleString = new char[size];
	strcpy_s(szSampleString, size, str);

	ULONG ulSize = strlen(szSampleString) + sizeof(char);
	char* pszReturn = NULL;

	pszReturn = (char*)::CoTaskMemAlloc(ulSize);
	strcpy_s(pszReturn, ulSize, szSampleString);

	delete[] szSampleString;

	return pszReturn;
}
#pragma endregion

#define DATA_SIZE 5096

char __enclave_data[DATA_SIZE];
char __frd_fpath[256];

#pragma region Logging
#define LOG_SIZE 256 * 256

char __log[LOG_SIZE];
bool __log_is_initialized = false;

void add_log(char* value) {
	if (!__log_is_initialized) {
		strcpy_s(__log, "[FREY EXECUTION LOG]\n");
		__log_is_initialized = true;
	}
	strcat_s(__log, value);
}

void add_log(char value) {
	for (int i = 0; i < LOG_SIZE - 1; ++i) {
		if (__log[i] == '\0') {
			__log[i] = value;
			__log[i + 1] = '\0';
			return;
		}
	}
}

void add_log_line(char* prefix, char* content, char* suffix) {
	strcat_s(__log, prefix);
	strcat_s(__log, content);
	strcat_s(__log, suffix);
	strcat_s(__log, "\n");
}

void add_log_star_line() {
	strcat_s(__log, "*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*\n");
}

void add_gbuf_stat_to_log() {
	add_log("gbuf stat: (START)");
	add_log(__enclave_data);
	add_log("(END)\n");
}

void print_log() {
	printf("%s", __log);
}

void print_gbuf_stat() {
	printf("gbuf stat: %s\n", __enclave_data);
}

void output_log(char* log_fpath) {
	FILE *fp;
	fopen_s(&fp, log_fpath, "w");
	if (fp == NULL) {
		add_log("error: the file could not be opened.\n");
		exit(1);
	}
	fprintf(fp, __log);
	fclose(fp);
}

void clear_log() {
	strcpy_s(__log, "");
}

extern "C" __declspec(dllexport) char*  __stdcall get_log() {
	add_log("called func: get_log\n");
	return str_export(__log, LOG_SIZE);
}
#pragma endregion 

#pragma region Encryption
char __encryption_key_store[DATA_SIZE];
char __decryption_key_store[DATA_SIZE];

void set_encryption_key(char* key) {
	add_log("called func: set_encryption_key\n");
	add_log_line("(-arg1: key) ", key, " : char*");
	strcpy_s(__encryption_key_store, key);
}

void set_decryption_key(char* key) {
	add_log("called func: set_decryption_key\n");
	add_log_line("(-arg1: key) ", key, " : char*");
	strcpy_s(__decryption_key_store, key);
}

char* encrypt(char *src) {
	add_log("called func: encrypt\n");
	add_log_line("(-arg1: src) ", src, " : char*");
	int src_len = strlen(src);
	int key_len = strlen(__encryption_key_store);
	int key_pos = 0, i;
	char dest[DATA_SIZE];
	strcpy_s(dest, src);
	for (i = 0; i < src_len; i++, key_pos++) {
		if (key_pos > key_len - 1) {
			key_pos = 0;
		}
		add_log("\t[ENCRYPTING...] ");
		add_log(dest);
		dest[i] = src[i] ^ __encryption_key_store[key_pos];
		add_log(" (");
		add_log(src[i]);
		add_log(" -> ");
		add_log(dest[i]);
		add_log(" by ");
		add_log(__encryption_key_store[key_pos]);
		add_log(")\n");
	}
	add_log_star_line();
	add_log_line("# origin data:\n", src, "");
	add_log_star_line();
	add_log_line("# encrypted data:\n", dest, "");
	add_log_star_line();
	return dest;
}

char* decrypt(char *src) {
	add_log("called func: decrypt\n");
	add_log_line("(-arg1: src) ", src, " : char*");
	int src_len = strlen(src);
	int key_len = strlen(__decryption_key_store);
	int key_pos = 0, i;
	char dest[DATA_SIZE];
	strcpy_s(dest, src);
	for (i = 0; i < src_len; i++, key_pos++) {
		if (key_pos > key_len - 1) {
			key_pos = 0;
		}
		add_log("\t[DECRYPTING...] ");
		add_log(dest);
		dest[i] = src[i] ^ __decryption_key_store[key_pos];
		add_log(" (");
		add_log(src[i]);
		add_log(" -> ");
		add_log(dest[i]);
		add_log(" by ");
		add_log(__decryption_key_store[key_pos]);
		add_log(")\n");
	}
	add_log_star_line();
	add_log_line("# origin data:\n", src, "");
	add_log_star_line();
	add_log_line("# decrypted data:\n", dest, "");
	add_log_star_line();
	return dest;
}
#pragma endregion

#pragma region Frey Base Funcs
int frey_init() {
	add_log("called func: frey_init\n");
	if (initialize_enclave() < 0) {
		return -1;
	}
}

void frey_finalize() {
	add_log("called func: frey_finalize\n");
	sgx_destroy_enclave(global_eid);
}
#pragma endregion 

#pragma region Write Call
extern "C" __declspec(dllexport) void __stdcall frey_write_call(char* data, char* frd_fpath, char* encryption_key, char* log_fpath = NULL) {
	add_log("called func: frey_write_call\n");
	add_log("(-arg1: data) ");
	add_log(data);
	add_log(" : char*\n");
	add_log("(-arg2: frd_fpath) ");
	add_log(frd_fpath);
	add_log(" : char*\n");
	add_log("(-arg3: encryption_key) ");
	add_log(encryption_key);
	add_log(" : char*\n");
	add_log("(-arg4: log_fpath) ");
	add_log(log_fpath);
	add_log(" : char*\n");
	strcpy_s(__frd_fpath, frd_fpath);
	set_encryption_key(encryption_key);
	frey_init();
	strcpy_s(__enclave_data, encrypt(data));
	frey_write(global_eid);
	frey_finalize();
	if (log_fpath != NULL) {
		output_log(log_fpath);
	}
	clear_log();
	return;
}

void frey_write_source_ocall(void *sc, size_t size){
	add_log("called func: frey_write_source_ocall\n");
	FILE *fp;
	fopen_s(&fp, __frd_fpath, "wb");
	if (fp == NULL) {
		add_log("error: the file could not be opened.\n");
	}
	fwrite(__enclave_data, sizeof(char), sizeof(__enclave_data) / sizeof(__enclave_data[0]), fp);
	fclose(fp);
}
#pragma endregion

#pragma region Read Call
extern "C" __declspec(dllexport) char* __stdcall frey_read_call(char* frd_fpath, char* decryption_key, char* log_fpath = NULL) {
	add_log("called func: frey_read_call\n");
	add_log("(-arg1: frd_fpath) ");
	add_log(frd_fpath);
	add_log(" : char*\n");
	add_log("(-arg2: decryption_key) ");
	add_log(decryption_key);
	add_log(" : char*\n");
	add_log("(-arg3: log_fpath) ");
	add_log(log_fpath);
	add_log(" : char*\n");
	strcpy_s(__frd_fpath, frd_fpath);
	set_decryption_key(decryption_key);
	frey_init();
	frey_read(global_eid);
	frey_finalize();
	if (log_fpath != NULL) {
		output_log(log_fpath);
	}
	clear_log();
	return str_export(__enclave_data, DATA_SIZE);
}

void frey_read_source_ocall(void *sc, size_t size) {
	add_log("called func: frey_read_source_ocall\n");
	FILE *fp;
	fopen_s(&fp, __frd_fpath, "r");
	if (fp == NULL) {
		strcpy_s(__enclave_data, "");
		return;
	}
	char c;
	int i = 0;
	char *out = (char *)sc;
	while ((c = (char)fgetc(fp)) != EOF) {
		out[i++] = c;
		if (i == size) {
			add_log("error: the file size exceeds more bytes\n");
		}
	}
	strcpy_s(__enclave_data, decrypt(out));
	fclose(fp);
}
#pragma endregion

#pragma region Tests
extern "C" __declspec(dllexport) bool __stdcall is_aval_test() {
	add_log("called func: is_aval_test\n");
	return true;
}

extern "C" __declspec(dllexport) char* __stdcall encrypt_test(char* value, char* key) {
	add_log("called func: encrypt_test\n");
	set_encryption_key(key);
	return encrypt(value);
}

extern "C" __declspec(dllexport) char* __stdcall decrypt_test(char* value, char* key) {
	add_log("called func: decrypt_test\n");
	set_decryption_key(key);
	return decrypt(value);
}
#pragma endregion
