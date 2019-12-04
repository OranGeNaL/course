#include <iostream>
#include <string>
#include <string.h>
#include <fstream>

using namespace std;

struct Note
{
public:
    char contributorData[30] = "";
    unsigned short int contributionSumm;
    char contributionDate[10] = "";
    char lawyerData[22] = "";

    Note()
    {
        
    }

private:
};

struct ListElement
{
    Note *note;
    ListElement *previousElement;
    ListElement *nextElement;
};

int main()
{
    string inputPath = "";

    ifstream file;
    file.open(inputPath);

    if(!file)
    {
        cout << "Invalid file!\n";
        return 0;
    }
}