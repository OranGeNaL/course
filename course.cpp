#include <iostream>
#include <string>
#include <string.h>
#include <fstream>

using namespace std;

void Fixer(char *, int);
struct Note
{
public:
    char contributorData[30];
    unsigned short int contributionSumm;
    char contributionDate[10];
    char lawyerData[22];

    Note(char contrData[30], unsigned short int contrSumm, char *contrDate, char *lawData)
    {
        strcpy(contributorData, contrData);
        contributionSumm = contrSumm;
        strcpy(contributionDate, contrDate);
        strcpy(lawyerData, lawData);
    }
    Note()
    {

    }

private:
};

struct ListElement
{
public:
    Note note;
    ListElement *nextElement = NULL;

    ListElement(Note valueField)
    {
        note = valueField;
    }
    void ShowList()
    {
        cout << note.contributorData << " "
             << note.contributionSumm << " "
             << note.contributionDate << " "
             << note.lawyerData << endl;
        if (nextElement)
        {
            nextElement->ShowList();
        }
    }

private:
};

int main()
{
    ListElement *head = NULL;
    ListElement *tail = NULL;
    string inputPath = "";

    cout << "Enter input file path: ";
    cin >> inputPath;

    ifstream file;
    file.open(inputPath);

    if (!file)
    {
        cout << "Invalid file!\n";
        return 0;
    }

    while (!file.eof())
    {
        char contributorData[30] = "";
        unsigned short int contributionSumm = NULL;
        char contributionDate[10] = "";
        char lawyerData[22] = "";

        file >> contributorData;
        file >> contributionSumm;
        file >> contributionDate;
        file >> lawyerData;

        Fixer(contributorData, 30);
        Fixer(lawyerData, 22);

        Note newNote = *new Note(contributorData, contributionSumm, contributionDate, lawyerData);
        if (!head)
        {
            head = new ListElement(newNote);
            //tail->ShowList();
        }
        else if (!tail)
        {
            tail = new ListElement(newNote);
            head->nextElement = tail;
        }
        else
        {
            tail->nextElement = new ListElement(newNote);
            tail = tail->nextElement;
            //tail->ShowList();
        }
    }

    ListElement *tempElement = head;

    while(tempElement->nextElement)
    {
        if(tempElement->nextElement == tail)
        {
            tempElement->nextElement = NULL;
            tail = tempElement;
            break;
        }
        tempElement = tempElement->nextElement;
    }

    head->ShowList();
    // cout << tail->note.contributorData << endl;
    // cout << head->note.contributorData << " " << head->nextElement->note.contributorData << endl;
}



void Fixer(char *input, int length)
{
    for (int i = length - strlen(input); i > 1; i--)
    {
        input[length - i] = '_';
    }
    input[strlen(input)] = '\0';
}