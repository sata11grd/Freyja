// Tester.cpp : このファイルには 'main' 関数が含まれています。プログラム実行の開始と終了がそこで行われます。
//

#include <iostream>
#include <windows.h>
#include <filesystem>
#include <string>
#include <filesystem>
#include <stdio.h>

typedef int(*FUNC)(int a, int b);

int existFile(const char* path)
{
	FILE* fp;
	fopen_s(&fp, path, "r");
	if (fp == NULL) {
		return 0;
	}

	fclose(fp);
	return 1;
}

int main()
{
	HMODULE hModule = LoadLibrary("ChildDll.dll");
	if (hModule == NULL) return -1;

	FUNC func = (FUNC)GetProcAddress(hModule, "childAdd");

	std::cout << func(3, 5);

	return 0;
}
