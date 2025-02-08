#include "GA.h"
#include<iostream>
#include<vector>

// ������������� ���������� ���������� � �������� �����
int size = 8;
std::random_device rd;
std::mt19937 gen(rd());
std::uniform_int_distribution<> dis(0, size - 1);

// ���������� ������� ������
Individ::Individ() {
	queens = std::vector<int>(size);
	for (auto i = 0; i < queens.size(); ++i) {
		queens[i] = dis(gen);
	}
	calcFitness();
}

void Individ::calcFitness() {
	auto cntConflicts = 0;
	for (auto i = 0; i < size; ++i) {
		for (auto j = i + 1; j < size; ++j) {
			if (queens[i] == queens[j] || (abs(queens[i] - queens[j]) == abs(i - j)))
				++cntConflicts;
		}
	}
	fitness = cntConflicts;
}


bool Individ::operator<(const Individ& other) const {
	return this->fitness < other.fitness;
}

void Individ::print() const {
	for (auto i = 0; i < size; ++i) {
		for (auto j = 0; j < size; ++j) {
			if (queens[i] == j)
				std::cout << "Q  ";
			else
				std::cout << "E  ";
		}
		std::cout << '\n';
	}
	std::cout << '\n';
}


Individ cross(Individ& par1, Individ& par2) {
	Individ child;
	//����� �������� �� ������� "��������"
	for (auto i = 0; i < size / 2; ++i) 
		child.queens[i] = par1.queens[i];

	//����� �������� �� ������� "��������
	for (auto i = size / 2; i < size; ++i)
		child.queens[i] = par2.queens[i];
    child.calcFitness();
	return child;
}

void mutate(Individ& individ){
	auto r = dis(gen); // ����� ��������� ������
	individ.queens[r] = dis(gen); // �������� ������������ �����
	individ.calcFitness(); // ����������� ���������� ���������� ����� ���������� �����
}

Individ ga(int populationSize, int generationsCnt, double mutationRate){
	std::vector<Individ> pop(populationSize);

	//������������� ������ ���������
	for (auto i = 0; i < populationSize; ++i) 
		pop[i] = Individ();
	
	for (auto g = 0; g < generationsCnt; ++g) {
		//����������� �� ����������� fitness
		std::sort(pop.begin(), pop.end());
		
		if (pop[0].fitness == 0) {
			std::cout << "������� ������� � ��������� " << g << '\n';
			return pop[0];
		}

		//������� ����� ���������
		std::vector<Individ> newPop(populationSize);
		for (auto i = 0; i < populationSize / 2; ++i) {
			auto par1 = pop[i]; // ����� �������
			auto par2 = pop[populationSize - i - 1]; // ����� � �����
			auto newIndivid = cross(par1, par2); // ���������� ���������
			if (dis(gen) < size * mutationRate) // �������� � ������������ ������������
				mutate(newIndivid);

			newPop.push_back(newIndivid);
		}
		pop = newPop; // �������� � ����� ����������
	}
	std::cout << "������� �� �������" << '\n';
	return Individ();
}



state2::state2() {
	queens = std::vector<int>(size);
	for (auto i = 0; i < queens.size(); ++i) {
		queens[i] = dis(gen);
	}
	calcFitness();
}

void state2::calcFitness() {
	auto cntConflicts = 0;
	for (auto i = 0; i < size; ++i) {
		for (auto j = i + 1; j < size; ++j) {
			if (queens[i] == queens[j] || (abs(queens[i] - queens[j]) == abs(i - j)))
				++cntConflicts;
		}
	}
	fitness = cntConflicts;
}

state2 state2::next(){
	auto neighbor = *this;
	int row = dis(gen);
	int newCol = dis(gen);
	neighbor.queens[row] = newCol;
	neighbor.calcFitness();
	return neighbor;
}

void state2::print() const {
	for (auto i = 0; i < size; ++i) {
		for (auto j = 0; j < size; ++j) {
			if (queens[i] == j)
				std::cout << "Q  ";
			else
				std::cout << "E  ";
		}
		std::cout << '\n';
	}
	std::cout << '\n';
}

state2 annealing(double temper, double coolRate){
	auto cnt = 0;
	auto curState = state2();
	while (temper > 1.0) {
		++cnt;
		auto nextState = curState.next();
		auto d = nextState.fitness - curState.fitness; // ���� d < 0, �� ��������� ��������� �����

		if (d < 0 || exp(-d / temper) >(double) rand() / RAND_MAX) // ���� ����� ������ ���������, �� ���������. ���� ����, �� ��������� � ������������
			curState = nextState;
		if (curState.fitness == 0)
		{
			std::cout << "���������� ��������: " << cnt << '\n';
			return curState;

		}
		temper *= coolRate;
	}
	std::cout << "�� �������" << '\n';
	return state2();
}