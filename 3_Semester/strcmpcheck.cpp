#include <iostream>
#include <string>
#include <string.h>
#include <stdlib.h>
#include <stdio.h>
#include <fstream>

using namespace std;

int main()
{
    char str1[] = "Abf";
    char str2[] = "Aag";

    int res = 0;
    res = strcmp(str1, str2);
    cout << res << endl;
}