#pragma comment(lib, "FreyApp.lib")
#pragma comment(lib, "FreyEnclave.lib")

#include <iostream>
#include <Windows.h>
#include <tchar.h>
#include <stdio.h>

extern "C" __declspec(dllimport) void __stdcall frey_write_call(char* data, char* frd_fpath, char* encryption_key, char* log_fpath = NULL);
extern "C" __declspec(dllimport) char* __stdcall frey_read_call(char* frd_fpath, char* decryption_key, char* log_fpath = NULL);

#define LOG_SIZE 1024

int main()
{
	//char data[] = "[name]jhon doe:System.String\n[hp]100:System.Int32\n[mp]50:System.Int32\n[spd]1.21:System.Single";
	char data[] = "[name]jhondoe:System.String[hp]100:System.Int32[mp]50:System.Int32[spd]1.21:System.Single";
	char frd_fpath[] = "C:\\Users\\sfuna\\Desktop\\__frey_tester__.frd";
	char log_fpath[] = "C:\\Users\\sfuna\\Desktop\\__frey_tester__frey.log";
	char key[] = "j7sd6oE";

	frey_write_call(data, frd_fpath, key, log_fpath);
	std::cout << frey_read_call(frd_fpath, key, log_fpath) << std::endl;

	getchar();

	return 0;
}