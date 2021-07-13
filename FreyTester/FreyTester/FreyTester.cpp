#pragma comment(lib, "App.lib")
#pragma comment(lib, "Enclave.lib")

#include <iostream>
#include <Windows.h>
#include <tchar.h>

extern "C" __declspec(dllimport) char* __stdcall frey_write_call_test(char*, char*);

#define LOG_SIZE 1024

int main()
{
	char data[] = "awjijdkljfioeujklfsdlkfjlijselfe";
	char fpath[] = "C:\\Users\\sfuna\\Desktop\\frey.edf";

	std::cout << frey_write_call_test(data, fpath) << std::endl;

	return 0;
}