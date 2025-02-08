#include "GA.h"
#include<iostream>
#include<vector>

// Инициализация глобальных переменных в исходном файле
int size = 8;
std::random_device rd;
std::mt19937 gen(rd());
std::uniform_int_distribution<> dis(0, size - 1);

// Реализация методов класса
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
	//берем половину от первого "родителя"
	for (auto i = 0; i < size / 2; ++i) 
		child.queens[i] = par1.queens[i];

	//берем половину от второго "родителя
	for (auto i = size / 2; i < size; ++i)
		child.queens[i] = par2.queens[i];
    child.calcFitness();
	return child;
}

void mutate(Individ& individ){
	auto r = dis(gen); // берем случайную строку
	individ.queens[r] = dis(gen); // случайно переставляем ферзя
	individ.calcFitness(); // пересчитали количество конфликтов после обновления доски
}

Individ ga(int populationSize, int generationsCnt, double mutationRate){
	std::vector<Individ> pop(populationSize);

	//инициализация первой популяции
	for (auto i = 0; i < populationSize; ++i) 
		pop[i] = Individ();
	
	for (auto g = 0; g < generationsCnt; ++g) {
		//отсортируем по возрастанию fitness
		std::sort(pop.begin(), pop.end());
		
		if (pop[0].fitness == 0) {
			std::cout << "Решение найдено в поколении " << g << '\n';
			return pop[0];
		}

		//создаем новую популяцию
		std::vector<Individ> newPop(populationSize);
		for (auto i = 0; i < populationSize / 2; ++i) {
			auto par1 = pop[i]; // берем сначала
			auto par2 = pop[populationSize - i - 1]; // берем с конца
			auto newIndivid = cross(par1, par2); // скрещиваем индивидов
			if (dis(gen) < size * mutationRate) // мутируем с определенной вероятностью
				mutate(newIndivid);

			newPop.push_back(newIndivid);
		}
		pop = newPop; // работаем с новой популяцией
	}
	std::cout << "Решение не найдено" << '\n';
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
		auto d = nextState.fitness - curState.fitness; // если d < 0, то следующее состояние лучше

		if (d < 0 || exp(-d / temper) >(double) rand() / RAND_MAX) // если нашли лучшее состояние, то принимаем. Если хуже, то принимаем с вероятностью
			curState = nextState;
		if (curState.fitness == 0)
		{
			std::cout << "Количество итераций: " << cnt << '\n';
			return curState;

		}
		temper *= coolRate;
	}
	std::cout << "Не найдено" << '\n';
	return state2();
}