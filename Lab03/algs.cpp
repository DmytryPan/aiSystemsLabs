#include "algs.h"
#include<iostream>
#include<queue>
#include<stack>
state bfs(int size = 8) {
	int cnt = 0;

	std::queue<state> queue;
	auto start = state(size);
	queue.push(start);
	while (!queue.empty()) {
		++cnt;
		auto currentState = queue.front();
		queue.pop();

		if (currentState.is_finish()) {
			std::cout << "Узлов рассмотрено: " << cnt << '\n';
			return currentState;
		}

		for (auto col = 0; col < size; ++col) {
			if (currentState.canPlace(currentState.getCntRow(), col)) {
				auto nextState = currentState.next(col);
				queue.push(nextState);
			}
		}
			
	}
}


state dfs(int size) {
	int cnt = 0;
	std::stack<state> st;
	auto start = state(size);
	st.push(start);

	while (!st.empty()) {
		++cnt;
		auto currentState = st.top();
		st.pop();

		if (currentState.is_finish()) {
			std::cout << "Узлов рассмотрено: " << cnt << '\n';
			return currentState;
		}

		for(auto indCol = size - 1; indCol >= 0; --indCol){
			if (currentState.canPlace(currentState.getCntRow(), indCol)) {
				auto next = currentState.next(indCol);
				st.push(next);
			}

		
		}
	}
}

state ids(int size) {
	int cnt = 0;
	for (auto depth = 0; depth <= size; ++depth) {
		std::stack<state> st;
		auto start = state(size);
		st.push(start);
		while (!st.empty()) {
			++cnt;
			auto currentState = st.top();
			st.pop();

			if (currentState.is_finish()) {
				std::cout << "Узлов рассмотрено: " << cnt << '\n';
				std::cout << "Глубина: " << depth << '\n';
				return currentState;
			}

			if (currentState.getCntRow() < depth) {
				for (auto indCol = size - 1; indCol >= 0; --indCol) {
					if (currentState.canPlace(currentState.getCntRow(), indCol)) {
						auto next = currentState.next(indCol);
						st.push(next);
					}
				}
			}
		}
	}
	std::cout << "не найдено" << '\n';
	return state(size);
}