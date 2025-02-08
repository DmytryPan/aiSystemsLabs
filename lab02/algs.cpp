#include <iostream>
#include "algs.h"
#include<unordered_map>
#include<queue>
#include<stack>
#include<cmath>
//Глобальные переменные

int SIZE = 4; // размер поля 4*4
uint64_t fieldInt; // поле в виде 64 битного числа
std::string StartPos; // Стартовая расстановка в виде строки
unsigned char field[16]; 
unsigned char zeroPos;
int posFinal[16];
int posCols[16];
int posRows[16];
int mDist[16][16];
uint64_t posMask[16]; 
uint64_t endMask; // конечная маска

/// <summary>
/// Проверка на то, что исходная расстановка, вообще говоря, разрешима
/// </summary>
/// <param name="input_position">Входная расстановка</param>
/// <returns></returns>

bool is_solvable(std::string input_position) {
	auto pos_e = input_position.find('0') / SIZE + 1; // позиция пустой ячейки
	auto inv = 0; // количество инверсий
	for (auto i = 0; i < input_position.size(); ++i) {
		for (auto j = i + 1; j < input_position.size(); ++j) {
			if (input_position[i] == '0' || input_position[j] == '0')
				continue;
			if (input_position[i] > input_position[j])
				inv++;
		}
	}
	return (pos_e + inv) % 2 == 0;
}

void fillField(std::string start, unsigned char fld[16]) {
	StartPos = start;
	fieldInt = 0;
	for (auto i = 0; i < 16; ++i) {
		if (isdigit(start[i])) {
			fieldInt += (uint64_t)(start[i] - '0') << i * 4;
			fld[i] = start[i] - '0';
			if (start[i] == '0')
				zeroPos = i;
		}
		else {
			fieldInt += (uint64_t)(10 + start[i] - 'A') << i * 4;
			fld[i] = 10 + (start[i] - 'A');
		}
	}
}

void print(uint64_t fld) {
	for (auto i = 0; i < 4; ++i) {
		for (auto j = 0; j < 4; ++j) {
			auto p = (fld >> ((i * 4 + j) * 4)) & 0x0F;  // вычисляем порядковый номер и оставляем последние 4 бита
			if (p < 10)
				std::cout << " " << (int)p << " ";
			else
				std::cout << " " << (char)('A' + p - 10) << " ";
			if (j < 3)
				std::cout << '|';
		}
		std::cout << '\n';
		if (i<3)
			std::cout << "---|---|---|---" << '\n';
	}
}

uint8_t* getElems(uint64_t m, uint8_t r[16]) {
	for (int i = 0; i < 16; ++i) {
		r[i] = uint8_t((m & posMask[i]) >> (i * 4));
	}
	return r;
}


movement movements[4] = { UP, DOWN, LEFT, RIGHT };

bool up(uint64_t& f, unsigned char& zero) {
	if (zero > 3) {
		int newZero = zero - 4;
		uint64_t tile = (f >> (newZero * 4)) & 0xF; // Извлекаем плитку
		f &= ~(0xFULL << (newZero * 4));           // Удаляем плитку
		f |= (tile << (zero * 4));                 // Перемещаем плитку на новую позицию
		zero = newZero;
		return true;
	}
	return false;
}

bool down(uint64_t& f, unsigned char& zero) {
	if (zero < 12) {
		int newZero = zero + 4;
		uint64_t tile = (f >> (newZero * 4)) & 0xF; // Извлекаем плитку
		f &= ~(0xFULL << (newZero * 4));           // Удаляем плитку
		f |= (tile << (zero * 4));                 // Перемещаем плитку на новую позицию
		zero = newZero;
		return true;
	}
	return false;
}

bool left(uint64_t& f, unsigned char& zero) {
	if (zero % 4 != 0) {
		int newZero = zero - 1;
		uint64_t tile = (f >> (newZero * 4)) & 0xF; // Извлекаем плитку
		f &= ~(0xFULL << (newZero * 4));           // Удаляем плитку
		f |= (tile << (zero * 4));                 // Перемещаем плитку на новую позицию
		zero = newZero;
		return true;
	}
	return false;
}

bool right(uint64_t& f, unsigned char& zero) {
	if (zero % 4 != 3) {
		int newZero = zero + 1;
		uint64_t tile = (f >> (newZero * 4)) & 0xF; // Извлекаем плитку
		f &= ~(0xFULL << (newZero * 4));           // Удаляем плитку
		f |= (tile << (zero * 4));                 // Перемещаем плитку на новую позицию
		zero = newZero;
		return true;
	}
	return false;
}

// функция для вызова нужного действия
bool moves(movement m, uint64_t& f, uint8_t& zero) {
	switch (m) {
	case UP:
		return up(f, zero);
	case DOWN:
		return down(f, zero);
	case LEFT:
		return left(f, zero);
	case RIGHT:
		return right(f, zero);
	default:
		return false;
	}
}


//Алгоритмы
// BFS 
uint8_t bfs() {
	auto cnt = 0;
	std::unordered_map<uint64_t, state*> visited;
	std::queue<state*> queue;

	auto start_state = new state(fieldInt, zeroPos, NONE, nullptr, 0, 0);
	queue.push(start_state);
	visited[fieldInt] = start_state;

	while (!queue.empty()) {
		++cnt;
		auto curState = queue.front();
		queue.pop();
		if (curState->fMask == endMask) {
			std::cout << "BFS: " << cnt << " Вершин рассмотрено\n";
			auto depth = curState->g;

			// Освобождаем память
			for (auto& s : visited) {
				delete s.second;
			}
			std::cout << "BFS количество ходов: " << (int)depth << " \n";
			return depth;
		}

		for (movement m : movements) {
			// Запрещаем обратные ходы
			if ((curState->lastMovement == UP && m == DOWN) ||
				(curState->lastMovement == DOWN && m == UP) ||
				(curState->lastMovement == LEFT && m == RIGHT) ||
				(curState->lastMovement == RIGHT && m == LEFT)) {
				continue; // Пропускаем обратное движение
			}

			auto nextMask = curState->fMask;
			auto nextZero = curState->zero;
			if (moves(m, nextMask, nextZero)) {
				if (!visited.contains(nextMask)) {
				//	print(nextMask);
				//	std::cout << "\n\n";
					auto nextState = new state(nextMask, nextZero, m, curState, curState->g + 1, 0);
					queue.push(nextState);
					visited[nextMask] = nextState;
				}
			}



		}
	}
	std::cout << "не найдено";
	return 0;
}

int ManhattanDistance(uint8_t elems[16]) {

	int sum = 0;
	for (int i = 0; i < 16; ++i) {
		if (elems[i] == 0)continue;
		sum += mDist[elems[i]][i];
	}
	return sum;
}

int m_dist_delta(const uint64_t newMask, const int oldZero, const int newZero){
	int element = int((newMask & posMask[oldZero]) >> (oldZero * 4));
	return mDist[element][oldZero] - mDist[element][newZero];
}



uint8_t Astar() {
	int cnt = 0;
	std::unordered_map<uint64_t, state*> visited;
	std::priority_queue<state*, vector<state*>, compareStates> queue;
	uint8_t elems[16];
	getElems(fieldInt, elems);
	auto start_state = new state(fieldInt, zeroPos, NONE, nullptr, 0, ManhattanDistance(elems));
	queue.push(start_state);
	visited[start_state->fMask] = start_state;

	while (!queue.empty()) {
		++cnt;
		auto curState = queue.top();
		queue.pop();

		if (curState->fMask == endMask) {
			std::cout << "A*: " << cnt << " Вершин рассмотрено\n";
			auto depth = curState->g;

			// Освобождаем память
			for (auto& s : visited) {
				delete s.second;
			}
			std::cout << "A* количество ходов: " << (int)depth << " \n";
			return depth;
		}
		for (movement m : movements) {
			// Запрещаем обратные ходы
			if ((curState->lastMovement == UP && m == DOWN) ||
				(curState->lastMovement == DOWN && m == UP) ||
				(curState->lastMovement == LEFT && m == RIGHT) ||
				(curState->lastMovement == RIGHT && m == LEFT)) {
				continue; // Пропускаем обратное движение
			}
			auto nextMask = curState->fMask;
			auto nextZero = curState->zero;
			if (moves(m, nextMask, nextZero)) {
				if (!visited.contains(nextMask)) {
					uint8_t elems[16];
					uint8_t oldElems[16];
					for (int i = 0; i < 16; ++i) {
						elems[i] = uint8_t((nextMask & posMask[i]) >> (i * 4));
						oldElems[i] = uint8_t((curState->fMask & posMask[i]) >> (i * 4));
					}
					int delta = m_dist_delta(nextMask, curState->zero, nextZero);
					auto nextState = new state(nextMask, nextZero, m, curState, curState->g + 1, curState->h+delta);
					queue.push(nextState);
					visited[nextMask] = nextState;
				}
			}
		
		}
		
	}
	std::cout << "Не найдено"<<'\n';
	return 0;
}


uint8_t IDS() {
	long long cnt = 0;
	int maxDepth = 1;

	while (true) {
		stack<state> st;
		auto startState = state(fieldInt, zeroPos, NONE, nullptr, 0,0);
		st.push(startState);
		while (!st.empty()) {
			++cnt;
			auto curState = st.top();
			st.pop();

			if (curState.g > maxDepth) {
				continue;
			}
			if (curState.fMask == endMask) {
				std::cout << "IDS: Вершин рассмотрено " << cnt << "\n";
				std::cout << "Количество ходов: " << (int)curState.g << "\n";
				return curState.g;
			}
			for (movement m : movements) {
				// Запрещаем обратные ходы
				if ((curState.lastMovement == UP && m == DOWN) ||
					(curState.lastMovement == DOWN && m == UP) ||
					(curState.lastMovement == LEFT && m == RIGHT) ||
					(curState.lastMovement == RIGHT && m == LEFT)) {
					continue; // Пропускаем обратное движение
				}

				auto nextMask = curState.fMask;
				auto nextZero = curState.zero;
				if (moves(m, nextMask, nextZero)) {
					auto newState = state(nextMask, nextZero, m, &curState, curState.g + 1, 0);
					st.push(newState);
				}
			}
		}
		++maxDepth;
		if (maxDepth > 50) {
			std::cout << "Не найдено";
			return 0;
		}

	}
}


uint8_t IDAstar(){
	long long cnt = 0;
	uint8_t elems[16];
	getElems(fieldInt, elems);
	auto bound = ManhattanDistance(elems);

	while (true) {

		std::stack<state> st;
		uint8_t minNextLimit = UINT8_MAX;
		auto startState = state(fieldInt, zeroPos, NONE, nullptr, 0, 0);
		st.push(startState);

		while (!st.empty()) {
			++cnt;
			auto curState = st.top();
			st.pop();

			uint8_t f = curState.g + curState.h;
			if (f > bound) {
				minNextLimit = std::min(minNextLimit, f);
				continue;
			}

			if (curState.fMask == endMask) {
				std::cout << "IDA*: Вершин рассмотрено " << cnt << "\n";
				std::cout << "Количество ходов: " << (int)curState.g << "\n";
				return curState.g;
			}
			for (movement m : movements) {
				// Запрещаем обратные ходы
				if ((curState.lastMovement == UP && m == DOWN) ||
					(curState.lastMovement == DOWN && m == UP) ||
					(curState.lastMovement == LEFT && m == RIGHT) ||
					(curState.lastMovement == RIGHT && m == LEFT)) {
					continue; // Пропускаем обратное движение
				}

				auto nextMask = curState.fMask;
				auto nextZero = curState.zero;
				if (moves(m, nextMask, nextZero)) {
					uint8_t nextElems[16];
					getElems(nextMask, nextElems);
					uint8_t h = ManhattanDistance(nextElems);

					auto newState = state(nextMask, nextZero, m, &curState, curState.g + 1, h);
					st.push(newState);
				}
			}

		}
		// Если новый предел не обновлён, значит решения нет
		if (minNextLimit == UINT8_MAX) {
			return bound;  // Решение не найдено
		}
		// Если предел слишком велик, возвращаем максимальное значение
		if (minNextLimit >= 62) {
			return UINT8_MAX;
		}
		bound = minNextLimit;
	}
}