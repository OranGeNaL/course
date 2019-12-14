#include <iostream>
#include <string>
#include <string.h>
#include <fstream>

using namespace std;

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

    ListElement()
    {
    }
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
    int ListLength(int length = 1)
    {
        if (nextElement)
        {
            nextElement->ListLength(length + 1);
        }
        else
        {
            return length;
        }
    }
    ListElement *ElementGetter(int ind)
    {
        if (ind > 0)
            nextElement->ElementGetter(ind - 1);
        else
            return this;
    }

private:
};

void Fixer(char *, int);
void DigitalSorting(int, ListElement **, ListElement **, ListElement *);

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
        char fullContributorData[100] = "";
        unsigned short int contributionSumm = NULL;
        char contributionDate[10] = "";
        char lawyerData[22] = "";

        file >> fullContributorData;
        file >> contributionSumm;
        file >> contributionDate;
        file >> lawyerData;

        if(strlen(fullContributorData) >= 30)
        {
            strncpy(contributorData, fullContributorData, 29);
        }
        else
        {
            strcpy(contributorData, fullContributorData);
        }
        
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

    while (tempElement->nextElement)
    {
        if (tempElement->nextElement == tail)
        {
            tempElement->nextElement = NULL;
            tail = tempElement;
            break;
        }
        tempElement = tempElement->nextElement;
    }

    head->ShowList();


    ListElement **indMass = new ListElement *[head->ListLength()];
    ListElement **tempSortMass = new ListElement *[head->ListLength()];
    for (int i = 0; i < head->ListLength(); i++)
    {
        indMass[i] = head->ElementGetter(i);
        tempSortMass[i] = indMass[i];
    }

    DigitalSorting(3, indMass, tempSortMass, head);
    printf("-----------------------------------------------------------------\n");
    for(int i = 0; i < head->ListLength(); i++)
    {
        cout << indMass[i]->note.contributorData << " "
             << indMass[i]->note.contributionSumm << " "
             << indMass[i]->note.contributionDate << " "
             << indMass[i]->note.lawyerData << endl;
    }
    delete (tempSortMass);
}

//TODO:

void DigitalSorting(int digit, ListElement **mass, ListElement **tempMass, ListElement *head)
{
    int ind = 0;
    if (digit % 2 == 0)
    {
        for (int i = 65; i < 123; i++)
        {
            if (i == 91 || i == 92 || i == 93 || i == 94)
            {
                continue;
            }
            for (int j = 0; j < head->ListLength(); j++)
            {
                if (tempMass[j]->note.contributorData[digit] - '0' + 48 == i)
                {
                    mass[ind] = tempMass[j];
                    ind++;
                }
            }
        }
    }
    else if (digit % 2 == 1)
    {
        for (int i = 65; i < 123; i++)
        {
            if (i == 91 || i == 92 || i == 93 || i == 94)
            {
                continue;
            }
            for (int j = 0; j < head->ListLength(); j++)
            {
                if (mass[j]->note.contributorData[digit] - '0' + 48 == i)
                {
                    tempMass[ind] = mass[j];
                    ind++;
                }
            }
        }
    }

    if (digit != 0)
    {
        DigitalSorting(digit - 1, mass, tempMass, head);
    }
}

void DigitalSorting()
{
    
}

void Fixer(char *input, int length)
{
    for (int i = length - strlen(input); i > 1; i--)
    {
        input[length - i] = '_';
    }
    input[strlen(input)] = '\0';
}