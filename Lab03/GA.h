#pragma once
#include<iostream>
#include<vector>
#include<random>

extern int size; // ќбъ€вление глобальной переменной

extern std::random_device rd; // ќбъ€вление, без инициализации
extern std::mt19937 gen;      // »нициализаци€ перенесена в cpp-файл
extern std::uniform_int_distribution<> dis; // “о же самое

class Individ {
public:
    std::vector<int> queens;
    int fitness;

    Individ();
    void calcFitness();
    bool operator<(const Individ& other) const;
    void print() const;
};

class state2 {
public:
	std::vector<int> queens;
	int fitness;

    state2();
	void calcFitness();
    state2 next();
    void print() const;
};


Individ cross(const Individ& par1, const Individ& par2);
void mutate(Individ& individ);
Individ ga(int populationSize, int generationsCnt, double mutationRate);

state2 annealing(double temper, double coolRate);