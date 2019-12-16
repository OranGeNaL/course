#include <iostream>
#include <string>
#include <string.h>
#include <stdlib.h>
#include <stdio.h>
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
void DigitalSorting(ListElement **, ListElement *);
int MaxLength(ListElement **, int);
void ISummToChSumm(char *, ListElement **, int, int);

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

        if (strlen(fullContributorData) >= 30)
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
    DigitalSorting(indMass, head);
    printf("-----------------------------------------------------------------\n");
    for (int i = 0; i < head->ListLength(); i++)
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

void DigitalSorting(ListElement **mass, ListElement *head)
{
    char lastName[15] = "";
    int firstEnter = 0, lastEnter = 0, lastNameLength = 0;

    for (int i = 0; i < head->ListLength(); i++)
    {
        if (!strcmp(lastName, ""))
        {
            for (int j = 0; j < strlen(mass[i]->note.contributorData); j++)
            {
                if (mass[i]->note.contributorData[j] != '_')
                {
                    lastName[j] = mass[i]->note.contributorData[j];
                    lastNameLength++;
                }
                else
                {
                    break;
                }
            }
            firstEnter = i;
            continue;
        }

        if(i == head->ListLength() - 1)
        {
            lastEnter = i;
        }

        char subStr[15] = "";
        strncpy(subStr, mass[i]->note.contributorData, lastNameLength);
        if (!strcmp(lastName, subStr))
        {
            lastEnter = i;
        }

        if (strcmp(lastName, subStr) || lastEnter == head->ListLength() - 1)
        {
            if (lastEnter - firstEnter >= 1)
            {
                ListElement **subMass = new ListElement *[lastEnter - firstEnter + 1];
                ListElement **tempSubMass = new ListElement *[lastEnter - firstEnter + 1];
                for (int j = 0; j <= lastEnter - firstEnter; j++)
                {
                    subMass[j] = mass[firstEnter + j];
                    tempSubMass[j] = subMass[j];
                }
                int cycleAmount = MaxLength(subMass, lastEnter - firstEnter + 1);
                for (int j = cycleAmount - 1; j >= 0; j--)
                {
                    int index = 0;
                    for (int k = 0; k <= 9; k++)
                    {
                        if (j % 2 == 0)
                        {
                            for (int h = 0; h <= lastEnter - firstEnter; h++)
                            {
                                char chSumm[15];
                                ISummToChSumm(chSumm, tempSubMass, h, cycleAmount);
                                if (chSumm[j] - '0' == k)
                                {
                                    subMass[index] = tempSubMass[h];
                                    index++;
                                }
                            }
                        }
                        else
                        {
                            for (int h = 0; h <= lastEnter - firstEnter; h++)
                            {
                                char chSumm[15];
                                ISummToChSumm(chSumm, subMass, h, cycleAmount);
                                if (chSumm[j] - '0' == k)
                                {
                                    tempSubMass[index] = subMass[h];
                                    index++;
                                }
                            }
                        }
                    }
                }

                for(int j = 0; j <= lastEnter - firstEnter; j++)
                {
                    mass[j + firstEnter] = subMass[j];
                }

                delete (subMass);
                delete (tempSubMass);
            }

            firstEnter = 0;
            lastEnter = 0;
            lastNameLength = 0;
            for (int j = 14; j >= 0; j--)
            {
                if (j != 0)
                {
                    lastName[j] = NULL;
                }
                else
                {
                    lastName[j] = '\0';
                }
            }
            i--;
        }
    }
}

void Fixer(char *input, int length)
{
    for (int i = length - strlen(input); i > 1; i--)
    {
        input[length - i] = '_';
    }
    input[strlen(input)] = '\0';
}

int MaxLength(ListElement **mass, int massLength)
{
    int maxLength = 0;
    for (int i = 0; i < massLength; i++)
    {
        char chSumm[15];
        sprintf(chSumm, "%d", mass[i]->note.contributionSumm);
        if (strlen(chSumm) > maxLength)
        {
            maxLength = strlen(chSumm);
        }
    }
    return maxLength;
}

void ISummToChSumm(char *chSumm, ListElement **mass, int ind, int maxLength)
{
    sprintf(chSumm, "%d", mass[ind]->note.contributionSumm);
    while (strlen(chSumm) < maxLength)
    {
        for (int i = 14; i >= 0; i--)
        {
            if (i != 0)
            {
                chSumm[i] = chSumm[i - 1];
            }
            else
            {
                chSumm[i] = '0';
            }
        }
    }
}