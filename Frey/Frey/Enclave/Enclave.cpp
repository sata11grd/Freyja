/*
 * Copyright (C) 2011-2019 Intel Corporation. All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 *
 *   * Redistributions of source code must retain the above copyright
 *     notice, this list of conditions and the following disclaimer.
 *   * Redistributions in binary form must reproduce the above copyright
 *     notice, this list of conditions and the following disclaimer in
 *     the documentation and/or other materials provided with the
 *     distribution.
 *   * Neither the name of Intel Corporation nor the names of its
 *     contributors may be used to endorse or promote products derived
 *     from this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 * "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 * LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
 * A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
 * OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
 * LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 * THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *
 */
#include "Enclave.h"
#include "Enclave_t.h" /* print_string */
#include <stdarg.h>
#include <stdio.h> /* vsnprintf */

#include <math.h>
#include <cstdlib>
#include <stdlib.h>
#include <iostream>
#include <sstream>
#include <assert.h>
#include <string.h>
#include <string>

#include "../App/App.h"

// shoudl be same for APP.cpp
#define MAX_JS_FILE_SIZE 10240
#define MAX_JS_FILE_NAME_LENGTH 256

char *mystrdup(const char* input){
  char *out = new char[strlen(input)+1];
  for(int i=0; i<strlen(input)+1; i++){
    out[i] = input[i];
  }
  //  strcpy(out, input);
  return out;
}
#define strdup mystrdup

class myostringstream{
public:
  std::string str(){ return "NOT_VALID";}

  myostringstream& operator<< (const void *test1){return *this;}
  myostringstream& operator<< (const std::string& test2){return *this;}
  myostringstream& operator<< (const char *test2){return *this;}
  myostringstream& operator<< (const int test2){return *this;}
};
#define ostringstream myostringstream

using namespace std;

/* 
 * printf: 
 *   Invokes OCALL to display the enclave buffer to the terminal.
 */
int freyPrintf(const char* fmt, ...)
{
    char buf[BUFSIZ] = { '\0' };
    va_list ap;
    va_start(ap, fmt);
    vsnprintf(buf, BUFSIZ, fmt, ap);
    va_end(ap);
    ocall_print_string(buf);
    return (int)strnlen(buf, BUFSIZ - 1) + 1;
}

//#ifdef __GNUC__
//#define vsprintf_s vsnprintf
#define sprintf_s snprintf
#define _strdup strdup
//#endif

#include "TinyJS.hpp"
#include "TinyJS_MathFunctions.hpp"
#include "TinyJS_Functions.hpp"

//-------------------------add ML&ORAM Functions
void ORAM_TEST1(CScriptVar *c, void *) {
    int opt = scGetInt("a");
    scReturnInt(opt);   
    printf("Call Oram.init(%d)\n", opt);
}

void ORAM_TEST2(CScriptVar *c, void *) {
    int opt = scGetInt("a");
    scReturnInt(opt);    
    printf("Call Oram.search(%d)\n", opt);
}

void ML_TEST(CScriptVar *c, void *) {
    int x = scGetInt("x");
    int y = scGetInt("y");
    int z = scGetInt("z");
    scReturnInt(x*y*z);
    printf("Call ML_TEST (%d, %d, %d)\n", x, y, z);
}

void registerMyFunctions(CTinyJS *tinyJS) {
  tinyJS->addNative("function Oram.init(a)", ORAM_TEST1, 0);  
  tinyJS->addNative("function Oram.search(a)", ORAM_TEST2, 0);
  tinyJS->addNative("function ML.test(x, y, z)", ML_TEST, 0);    
}
//--------------------------


void js_print(CScriptVar *v, void *userdata) {
  printf("> %s\n", v->getParameter("text")->getString().c_str());
  // replace it by Ocall
}

void js_dump(CScriptVar *v, void *userdata) {
    CTinyJS *js = (CTinyJS*)userdata;
    js->root->trace(">  ");
}

void cp_source(void *ptr, size_t len){
  std::string sc = (const char*)ptr;
  printf("%s", sc.c_str());
}

void exec_JS(){
  //set js source filename
  char buf[256];
  sprintf_s(buf, 256, "%s", "./test.js");  
  set_JS_Fname(buf);
  
  // Read js source file into ssc
  int size = MAX_JS_FILE_SIZE;
  char *sc = new char[size];
  cp_source_ocall((void *)sc, size);  
  std::string ssc = sc;
  delete[] sc;

  // Preparation for JS interpreter
  CTinyJS *js = new CTinyJS();  
  registerFunctions(js);
  registerMathFunctions(js);
  registerMyFunctions(js);    
  js->addNative("function print(text)", &js_print, 0);
  js->addNative("function dump()", &js_dump, js);  

  // Execute JS
  while(ssc.length()>0){
    std::string inst = ssc.substr(0, ssc.find("\n")+1);
    ssc = ssc.substr(ssc.find("\n")+1);
    // printf("%s", inst.c_str());
    js->execute(inst.c_str());
  }
  
  /**
  std::string com = "i = 0;";  
  js->execute(com.c_str());
  com = "";  
  js->execute(com.c_str());  
  com = "print(Math.sign(i));";
  js->execute(com.c_str());
  com = "print(Oram.init(1));";
  js->execute(com.c_str());  
  **/
  delete js;
  
}
