#pragma comment(lib, "App.lib")

#include <iostream>
#include <Windows.h>
#include <tchar.h>

extern "C" __declspec(dllimport) int __stdcall add(int a, int b);
extern "C" __declspec(dllimport) int __stdcall test();
extern "C" __declspec(dllimport) int __stdcall test2();

int main()
{
	std::cout << add(5, 4) << std::endl;
	std::cout << test() << std::endl;
	std::cout << test2() << std::endl;
}
