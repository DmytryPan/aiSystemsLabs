#include <iostream>
#include <vector>
#include <queue>
#include<unordered_set>
#include<set>
#include<chrono>

struct State {
	int value;
	int steps; // операции до текущего состояния
};

/*Даны два целых числа – например, 2 и 100, 
а также две операции – «прибавить 3» и «умножить на 2». 
Найти минимальную последовательность операций, 
позволяющую получить из первого числа второе.*/

int FindMinOps(int StartValue, int EndValue) {
	if (StartValue == EndValue)
		return 0;
	auto q = std::queue<State>();
	auto VisitedValues = std::unordered_set<int>();

	q.push({ StartValue, 0 });
	VisitedValues.insert(StartValue);

	while (!q.empty()) {
		auto current = q.front();
		q.pop();

		//*2
		auto NextValue = current.value * 2;
		if (NextValue == EndValue)
			return current.steps + 1;

		if (NextValue <= EndValue && VisitedValues.find(NextValue) == VisitedValues.end()) {
			VisitedValues.insert(NextValue);
			q.push({NextValue, current.steps+1});
		}

		// +3
		NextValue = current.value + 3;

		if (NextValue == EndValue)
			return current.steps + 1;

		if (NextValue <= EndValue && VisitedValues.find(NextValue) == VisitedValues.end()) {
			VisitedValues.insert(NextValue);
			q.push({NextValue, current.steps+1});
		}
	}
	return -1; // не нашли

}


int FindMinOps2(int StartValue, int EndValue) {
	if (StartValue == EndValue)
		return 0;
	auto q = std::queue<State>();
	auto VisitedValues = std::unordered_set<int>();

	q.push({ StartValue, 0 });
	VisitedValues.insert(StartValue);

	while (!q.empty()) {
		auto current = q.front();
		q.pop();

		//*2
		auto NextValue = current.value * 2;
		if (NextValue == EndValue)
			return current.steps + 1;

		if (NextValue <= EndValue && VisitedValues.find(NextValue) == VisitedValues.end()) {
			VisitedValues.insert(NextValue);
			q.push({ NextValue, current.steps + 1 });
		}

		// +3
		NextValue = current.value + 3;

		if (NextValue == EndValue)
			return current.steps + 1;

		if (NextValue <= EndValue && VisitedValues.find(NextValue) == VisitedValues.end()) {
			VisitedValues.insert(NextValue);
			q.push({ NextValue, current.steps + 1 });
		}

		//-2
		NextValue = current.value - 2;

		if (NextValue == EndValue)
			return current.steps + 1;

		if (NextValue <= EndValue && VisitedValues.find(NextValue) == VisitedValues.end()) {
			VisitedValues.insert(NextValue);
			q.push({ NextValue, current.steps + 1 });
		}
	}
	return -1; // не нашли

}

int FindMinOps3(int StartValue, int EndValue) {
	if (StartValue == EndValue)
		return 0;
	auto q = std::queue<State>();
	auto VisitedValues = std::unordered_set<int>();

	q.push({ EndValue, 0 });
	VisitedValues.insert(StartValue);

	while (!q.empty()) {
		auto current = q.front();
		q.pop();

		// -3
		auto NextValue = current.value - 3;
		if (NextValue == StartValue){
			std::cout <<"Founded: " << NextValue<<std::endl;
			return current.steps + 1;
		}

		if (NextValue >= StartValue && VisitedValues.find(NextValue) == VisitedValues.end()) {
			VisitedValues.insert(NextValue);
			q.push({ NextValue, current.steps + 1 });
		}

		// /2
		if (current.value % 2 == 0) {
			NextValue = current.value / 2;

			if (NextValue == StartValue){
				std::cout << "Founded: " << NextValue << std::endl;
				return current.steps + 1;
			}
			if (NextValue >= StartValue && VisitedValues.find(NextValue) == VisitedValues.end()) {
				VisitedValues.insert(NextValue);
				q.push({ NextValue, current.steps + 1 });
			}
		}
		
	}
	return -1; // не нашли

}



int main()
{
	
	std::vector<std::pair<int, int>> values1{std::make_pair(1, 100), 
		std::make_pair(2, 55), std::make_pair(2, 100), std::make_pair(1, 97),
	std::make_pair(2, 1000), std::make_pair(2, 10000001) };

	for (auto p : values1) {
		auto t0 = std::chrono::high_resolution_clock::now();
		std::cout << p.first <<" " <<p.second<<" :" << FindMinOps(p.first, p.second) << std::endl;
		auto t1 = std::chrono::high_resolution_clock::now();
		auto dur = std::chrono::duration_cast<std::chrono::milliseconds>(t1 - t0).count();
		std::cout << "time ms: "<< dur << std::endl;
	}

	std::cout << "------------------" << std::endl;
	/*
1 100 :7
time ms: 0
2 55 :6
time ms: 0
2 100 :7
time ms: 0
1 97 :8
time ms: 0
2 1000 :12
time ms: 0
2 10000001 :30
time ms: 1302
	*/


	std::vector<std::pair<int, int>> values2{ std::make_pair(1, 100),
	std::make_pair(2, 55), std::make_pair(2, 100), std::make_pair(1, 97),
	std::make_pair(2, 1000),std::make_pair(3, 1001),std::make_pair(3, 3001),
	std::make_pair(2, 10000001) };

	for (auto p : values2) {
		auto t0 = std::chrono::high_resolution_clock::now();
		std::cout << p.first << " " << p.second << " :" << FindMinOps2(p.first, p.second) << std::endl;
		auto t1 = std::chrono::high_resolution_clock::now();
		auto dur = std::chrono::duration_cast<std::chrono::milliseconds>(t1 - t0).count();
		std::cout << "time ms: " << dur << std::endl;
	}

	/*
1 100 :7
time ms: 1
2 55 :6
time ms: 0
2 100 :7
time ms: 0
1 97 :8
time ms: 0
2 1000 :11
time ms: 0
3 1001 :13
time ms: 0
3 3001 :14
time ms: 0
2 10000001 :30
time ms: 6832
	*/

	std::cout << "------------------" << std::endl;
	for (auto p : values1) {
		auto t0 = std::chrono::high_resolution_clock::now();
		std::cout << p.first << " " << p.second << " :" << FindMinOps3(p.first, p.second) << std::endl;
		auto t1 = std::chrono::high_resolution_clock::now();
		auto dur = std::chrono::duration_cast<std::chrono::milliseconds>(t1 - t0).count();
		std::cout << "time ms: " << dur << std::endl;
	}

	/*auto t0 = std::chrono::high_resolution_clock::now();
	std::cout << FindMinOps(2, 10000001) << std::endl;
	auto t1 = std::chrono::high_resolution_clock::now();
	auto dur = std::chrono::duration_cast<std::chrono::milliseconds>(t1 - t0).count();
	std::cout << "Время работы в мс: " << dur << std::endl;*/

}
