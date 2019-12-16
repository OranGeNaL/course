#include <iostream>
#include <string.h>
#include <string>
#include <fstream>
#include <time.h>
#include <stdlib.h>
#include "MyLib.cpp"

using namespace std;

int Randomizer(int, int);

string lastName[50] = {
    "Lueilwitz", "Monahan", "Rippin", "Witting", "Lehner",
    "Erdman", "Bailey", "Jenkins", "Carter", "Bradtke",
    "Fisher", "Spencer", "Boehm", "Ward", "Corwin",
    "Zboncak", "Ondricka", "Jakubowski", "Stehr", "Kuhlman",
    "Miller", "Ritchie", "Herman", "Will", "MacGyver",
    "Baumbach", "Auer", "Eichmann", "Renner", "Feeney",
    "Reichert", "Renner", "Kautzer", "Yost", "Purdy",
    "Waelchi", "Legros", "Turner", "Kirlin", "Swift",
    "Bernhard", "Weber", "Rutherford", "Prosacco", "Eichmann",
    "Effertz", "Corwin", "Lueilwitz", "Kris", "Weissnat"};

string firstName[30] = {
    "Jaren", "Rolando", "Jose", "Amara", "Liliana",
    "Aniya", "Thurman", "Orval", "Reilly", "Reta",
    "Augustine", "Tod", "Olga", "Dandre", "Demetris",
    "Bette", "Nya", "Collin", "Juanita", "Gregoria",
    "Bernhard", "Rupert", "Rhett", "Tressie", "Mason",
    "Mallie", "Rupert", "Mathilde", "Brandi", "Anabelle"};

string otchestvo[10] = {
    "Olegovich", "Artemovich", "Nikitievich", "Vadimovich", "Denisovich",
    "Valentinovich", "Maksimovich", "Vitalievich", "Evgenievich", "Sergeevich"};

int main()
{
    char path[] = "/home/orangenal/Документы/course/BaseNik";
    string result = "";
    srand(time(0));
    int K;
    cout << "Enter amount of notes: ";
    cin >> K;

    for (int i = 0; i < K; i++)
    {
        int day, month, year, randomazer;

        result += lastName[Randomizer(0, 50)] + "_";
        result += firstName[Randomizer(0, 30)] + "_";
        result += otchestvo[Randomizer(0, 10)] + "\n";

        result = result + to_string(Randomizer(50, 300)) + "\n";

        day = Randomizer(1, 31);
        month = Randomizer(1, 12);
        year = Randomizer(1, 99);

        if (day < 10)
            result += "0";
        result += to_string(day) + "-";
        if (month < 10)
            result += "0";
        result += to_string(month) + "-";
        if (year < 10)
            result += "0";
        result += to_string(year) + "\n";

        result += lastName[Randomizer(0, 50)] + "_";
        result = result + firstName[Randomizer(0, 30)][0] + "_";
        result = result + otchestvo[Randomizer(0, 10)][0] + "\n\n";
    }

    FileCreating(path, result);
}

int Randomizer(int left, int right)
{
    int randomValue = left + rand() % right;

    return randomValue;
}