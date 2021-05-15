#define _CRT_SECURE_NO_WARNINGS

#include <cstdio>
#include <cstring>
#include <iostream>
#include "Enclave_u.h"
#include "sgx_urts.h"
#include "error_print.h"
#include "Header.h"

sgx_enclave_id_t global_eid = 0;

extern "C" __declspec(dllexport) int add(int a, int b) {
	return a + b;
}

extern "C" __declspec(dllexport) int sub(int a, int b) {
	return a - b;
}

extern "C" __declspec(dllexport) int initialize_enclave() {
	std::string launch_token_path = "enclave.token";
	std::string enclave_name = "enclave.signed.so";

	const char* token_path = launch_token_path.c_str();

	sgx_launch_token_t token = { 0 };
	sgx_status_t status = SGX_ERROR_UNEXPECTED;

	int updated = 0;

	/*==============================================================*
	 * Step 1: Obtain enclave launch token                          *
	 *==============================================================*/

	// If exist, load the enclave launch token.
	FILE *fp = fopen(token_path, "rb");

	// If token doesn't exist, create the token.
	if (fp == NULL && (fp = fopen(token_path, "wb")) == NULL)
	{
		// Storing token is not necessary, so file I/O errors here is not fatal.
		std::cerr << "Warning: Failed to create/open the launch token file.";
	}

	if (fp != NULL)
	{
		// Read the token from saved file.
		size_t read_num = fread(token, 1, sizeof(sgx_launch_token_t), fp);

		// If token is invalid, clear the buffer.
		if (read_num != 0 && read_num != sizeof(sgx_launch_token_t))
		{
			memset(&token, 0x0, sizeof(sgx_launch_token_t));

			// As aforementioned, if token doesn't exist or is corrupted,
			// zero-flushed new token will be used for launch.
			// So token error is not fatal.
			std::cerr << "Warning: Invalid launch token read from launch_token_path.";
		}
	}
	return 0;
}