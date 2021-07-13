#pragma comment(lib, "App.lib")
#pragma comment(lib, "Enclave.lib")

#include <iostream>
#include <Windows.h>
#include <tchar.h>

extern "C" __declspec(dllimport) char* __stdcall encrypt_test(char*, char*);
extern "C" __declspec(dllimport) char* __stdcall decrypt_test(char*, char*);

#define LOG_SIZE 1024

int main()
{
	char plain_text[256] = "test";
	char key[256] = "s7eR";
	char res1[256];
	char res2[256];

	strcpy_s(res1, encrypt_test(plain_text, key));
	strcpy_s(res2, decrypt_test(res1, key));

	std::cout << res1 << std::endl;
	std::cout << res2 << std::endl;

	getchar();

	return 0;
}