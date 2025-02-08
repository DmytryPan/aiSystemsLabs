#pragma once
#include<iostream>
#include <vector>

class state {
public:
	// ����������� ��������� �����
	int size;
	// ������ � ������� ������� � ������ ������, ��� ����� ��������
	std::vector<int> queens;
	// ������� ��� �����
	int cntRow;

	//�����������
	state(int _size) : size(_size), queens(std::vector<int>(_size, -1)), cntRow(0) {}

	//�������� ������ ���������
	//� ������� ������ ������������� � ������� nCol �����
	//� ����������� cntRow 
	state next(int nCol) const {
		auto nextState = *this;
		nextState.queens[cntRow] = nCol; // ����� ������ � ������
		nextState.cntRow += 1;
		return  nextState;
	}
	
	//�������� �� ����������� ����������
	bool canPlace(int nRow, int nCol) const {
		for (auto i = 0; i < nRow; ++i) {
			auto selected_col = queens[i];
			if (selected_col == nCol || abs(selected_col - nCol) == abs(i - nRow)) //���� ������� ���� � ��� �� ���� �� ����� ���������
				return false;
		}
		return true;
	}
	//�������� �� ��������� ���������
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

	// ���������� ������� ����������� �����
	int getCntRow() const { return cntRow; }


};


//���������

state bfs(int size);
state dfs(int size);
state ids(int size);