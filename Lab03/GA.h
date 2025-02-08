#pragma once
#include<iostream>
#include<vector>
#include<random>

extern int size; // ���������� ���������� ����������

extern std::random_device rd; // ����������, ��� �������������
extern std::mt19937 gen;      // ������������� ���������� � cpp-����
extern std::uniform_int_distribution<> dis; // �� �� �����

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