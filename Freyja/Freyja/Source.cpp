extern "C" __declspec(dllexport) int __stdcall add(int a, int b) {
	return a + b;
}

extern "C" __declspec(dllexport) int __stdcall sub(int a, int b) {
	return a - b;
}