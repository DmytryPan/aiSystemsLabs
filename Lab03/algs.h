#pragma once
#include<iostream>
#include <vector>

class state {
public:
	// размерность шахматной доски
	int size;
	// Массив с метками позиций в каждой строке, где стоит королева
	std::vector<int> queens;
	// Счетчик для строк
	int cntRow;

	//конструктор
	state(int _size) : size(_size), queens(std::vector<int>(_size, -1)), cntRow(0) {}

	//Создание нового состояния
	//в текущей строке устанавливает в столбце nCol ферзя
	//и увеличивает cntRow 
	state next(int nCol) const {
		auto nextState = *this;
		nextState.queens[cntRow] = nCol; // пишем индекс в массив
		nextState.cntRow += 1;
		return  nextState;
	}
	
	//проверка на возможность размещения
	bool canPlace(int nRow, int nCol) const {
		for (auto i = 0; i < nRow; ++i) {
			auto selected_col = queens[i];
			if (selected_col == nCol || abs(selected_col - nCol) == abs(i - nRow)) //если столбец один и тот же либо на одной диагонали
				return false;
		}
		return true;
	}
	//проверка на найденное состояние
	bool is_finish() const {
		return cntRow == size;
	}

	void print() const {
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

	// возвращает счетчик заполненных строк
	int getCntRow() const { return cntRow; }


};


//Алгоритмы

state bfs(int size);
state dfs(int size);
state ids(int size);