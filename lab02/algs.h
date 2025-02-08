#pragma once
#include<iostream>
#include<string>

using namespace std;

extern uint64_t fieldInt;
extern unsigned char field[16];
extern unsigned char zeroPos;
extern uint64_t posMask[16];
extern uint64_t endMask; // �������� �����
extern int posFinal[16];
extern int posCols[16];
extern int posRows[16];
extern int mDist[16][16];


bool is_solvable(std::string input_position);

void fillField(std::string start, unsigned char f[16]);
void print(uint64_t fld);

bool up(uint64_t& f, unsigned char& zero);
bool down(uint64_t& f, unsigned char& zero);
bool left(uint64_t& f, unsigned char& zero);
bool right(uint64_t& f, unsigned char& zero);

//�����������
enum movement
{
	NONE,
	UP,
	DOWN,
	LEFT,
	RIGHT
};

struct state {
	uint64_t fMask; // ������� ��������� ���� � ���� 64 ������� �����
	uint8_t zero;  // ������� ������ ������ �� ����
	movement lastMovement; // ��������� ������������
	state* prev; // ���������� ���������
	uint8_t g; // ���������� ����
	uint8_t h; // ������������� ������

	state(uint64_t _fMask, uint8_t _zero, movement _lastMovement, state* _prev, uint8_t _g, uint8_t _h) :
		fMask(_fMask), zero(_zero), lastMovement(_lastMovement), prev(_prev), g(_g), h(_h) {
		
	}
};

uint8_t* getElems(uint64_t m, uint8_t r[16]);

//���������� ��� ������� � �����������
struct compareStates {
	bool operator()(const state* a, const state* b) const {
		return a->g + a->h > b->g + b->h;
	}
};

extern movement movements[4];




//���������

int ManhattanDistance(uint8_t elems[16]);

int m_dist_delta(const uint64_t newMask, const int oldZero, const int newZero);

//���������

uint8_t bfs();
uint8_t Astar();
uint8_t IDS();
uint8_t IDAstar();