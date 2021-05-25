extern "C" __declspec(dllimport) int __stdcall grandchildAdd(int a, int b);

extern "C" __declspec(dllexport) int __stdcall childAdd(int a, int b) {
	int buf = grandchildAdd(a, b);
	return buf;
}
