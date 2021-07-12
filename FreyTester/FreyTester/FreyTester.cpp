#pragma comment(lib, "App.lib")
#pragma comment(lib, "Enclave.lib")

#include <iostream>
#include <Windows.h>
#include <tchar.h>

extern "C" __declspec(dllimport) bool __stdcall is_aval_test();

#define LOG_SIZE 1024

int main()
{
	std::cout << is_aval_test() << std::endl;

	return 0;
}