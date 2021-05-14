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

	return 0;
}