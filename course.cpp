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
    ListElement *nextElement;

    ListElement(Note value, ListElement *next)
    {
        note = value;
        nextElement = next;
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
    ListElement *tail = head;
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
        char contributorData[30];
        unsigned short int contributionSumm;
        char contributionDate[10];
        char lawyerData[22];

        file >> contributorData;
        file >> contributionSumm;
        file >> contributionDate;
        file >> lawyerData;

        Fixer(contributorData, 30);
        Fixer(lawyerData, 22);

        Note newNote = *new Note(contributorData, contributionSumm, contributionDate, lawyerData);
        if (!head)
        {
            head = new ListElement(newNote, NULL);
            //tail->ShowList();
        }
        else if (!tail)
        {
            tail = new ListElement(newNote, NULL);
            head->nextElement = tail;
        }
        else
        {
            tail->nextElement = new ListElement(newNote, NULL);
            tail = tail->nextElement;
        }
    }

    head->ShowList();
    // cout << tail->note.contributorData << endl;
    // cout << head->note.contributorData << " " << head->nextElement->note.contributorData << endl;
}

void Fixer(char *input, int length)
{
    for (int i = strlen(input) - length; i > 0; i--)
    {
        input[strlen(input) - 1] = '_';
    }
}