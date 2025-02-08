#include <iostream>
#include <vector>

// размерность доски
const int rows = 6;
const int cols = 7;

const char playerLabel = 'X';
const char aiLabel = 'O';

const int maxDepth = 8; // ограничение глубины

struct movement {
    int col; // столбец в который опускаем фишку
    int row; // строка где лежит фишка
    int score; // оценка

    bool operator==(movement& other) {
        return (this->col == other.col) && (this->row == other.row) && (this->score == score);
    }

    bool operator!=(movement& other) {
        return !(*this == other);
    }
};

// Храним последние ходы, чтобы можно было их отменять
movement playerLastMovement;
movement aiLastMovement;

//печать доски
void printBoard(const std::vector<std::vector<char>>& board){
    for (auto& row : board) {
        for (auto& elem : row) {
            std::cout << (elem == ' ' ? '.' : elem) << " ";
        }
        std::cout << '\n';
    }
    std::cout << '\n';
}

//Проверка на победу
bool checkWin(const std::vector<std::vector<char>>& board, char label) {
    // победа в горизонталях
    for (int i = 0; i < rows; ++i) {
        for (int j = 0; j < cols-3; ++j) {
            if (board[i][j] == label &&
                board[i][j + 1] == label &&
                board[i][j + 2] == label &&
                board[i][j + 3] == label)
                return true;
        }
    }

    //победа в вертикалях
    for (int j = 0; j < cols; ++j) {
        for (int i = 0; i < rows - 3; ++i) {
            if (board[i][j] == label &&
                board[i + 1][j] == label &&
                board[i + 2][j] == label &&
                board[i + 3][j] == label)
                return true;
        }
    }

    //победа в диагоналях
    for (int i = 0; i < rows - 3; ++i) {
        for (int j = 0; j < cols - 3; ++j) {
            if (board[i][j] == label && 
                board[i + 1][j + 1] == label &&
                board[i + 2][j + 2] == label && 
                board[i + 3][j + 3] == label) {
                return true;
            }
        }
        for (int j = 3; j < cols; ++j) {
            if (board[i][j] == label && 
                board[i + 1][j - 1] == label &&
                board[i + 2][j - 2] == label && 
                board[i + 3][j - 3] == label) {
                return true;
            }
        }
    }
    return false;
}

//Проверка, есть ли место в столбце, куда делаем ход
bool canPlace(const std::vector<std::vector<char>>& board, int col) {
    return board[0][col] == ' ';
}

// делаем ход
movement makeMovement(std::vector<std::vector<char>>& board, int col, char label) {
    for (int row = rows - 1; row >= 0; --row) {
        if (board[row][col] == ' ') {
            board[row][col] = label;
            if (label == playerLabel)
                playerLastMovement = { col, row, 0 };
            else if (label == aiLabel)
                aiLastMovement = { col,row,  0 };
            return {col, row, 0};
        }
    }
    return { -1, -1, 0 };

}

void undoLastMovement(std::vector<std::vector<char>>& board, int row, int col) {
    board[row][col] = ' ';
}
//TODO: поификсить вертикали.
//Оценка окна из 4 позиций
int evalWindow(const std::vector<char>& window, char label) {
    int score = 0;
    char opp = (label == aiLabel) ? playerLabel : aiLabel;

    auto labelCnt = std::count(window.begin(), window.end(), label);
    auto emptyCnt = std::count(window.begin(), window.end(), ' ');
    auto oppCnt = std::count(window.begin(), window.end(), opp);

    if (labelCnt == 4)
        score += 1000;
    else if (labelCnt == 3 && emptyCnt == 1)
        score += 80;
    else if (labelCnt == 2 && emptyCnt == 2)
        score += 60;

    if (oppCnt == 3 && emptyCnt == 1)
        score -= 500;
    else if (oppCnt == 2 && emptyCnt == 2)
        score -= 100;

    return score;
}

int eval(std::vector<std::vector<char>>& board, int label) {
    int score = 0;

    //оценка центрального столбца
    for (int row = 0; row < rows; ++row)
        if (board[row][cols / 2] == label)
            score += 100;

    // горизонтальная оценка
    for (auto row = 0; row < rows; ++row)
        for (auto col = 0; col < cols - 3; ++col) {
            std::vector<char> window = { board[row][col], board[row][col + 1], board[row][col + 2], board[row][col + 3] };
            score += evalWindow(window, label);
        }

    // Вертикальная оценка
    for(auto col = 0; col < cols; ++col)
        for (auto row = 0; row < rows - 3; ++row) {
            std::vector<char> window = { board[row][col], board[row + 1][col], board[row + 2][col], board[row + 3][col] };
            score += evalWindow(window, label);
        }

    // Оценка диагоналей
    for (auto row = 0; row < rows - 3; ++row) {
        for (auto col = 0; col < cols - 3; ++col) {
            std::vector<char> window = { board[row][col], board[row + 1][col + 1], board[row + 2][col + 2], board[row + 3][col + 3] };
            score += evalWindow(window, label);
        }
        for (int col = 3; col < cols; ++col) {
            std::vector<char> window = { board[row][col], board[row + 1][col - 1], board[row + 2][col - 2], board[row + 3][col - 3] };
            score += evalWindow(window, label);
        }
    }
    return score;
}

std::vector<std::vector<char>> copyBoard(std::vector<std::vector<char>>& board) { return board; }

movement minimax(std::vector<std::vector<char>>& board, int depth, int alpha, int beta, bool maximizingPlayer) {
    if (depth == 0 || checkWin(board, aiLabel) || checkWin(board, playerLabel))
    {
        if (checkWin(board, aiLabel))
            return { -1, -1,1000 };
        if (checkWin(board, playerLabel))
            return { -1, -1, -1000 };
        return { -1, -1, eval(board, aiLabel) };
    }
    // 
    if (maximizingPlayer) {
        int maxEval = -100000;
        int bestCol = -1;
        for (auto col = 0; col < cols; ++col) {
            if (canPlace(board, col)) {
                auto boardCopy = copyBoard(board);
                auto lastMovement = makeMovement(boardCopy, col, aiLabel);
                int evalScore = minimax(boardCopy, depth - 1, alpha, beta, false).score;
                //undoLastMovement(board, lastMovement.row, lastMovement.col);
                if (evalScore > maxEval) {
                    maxEval = evalScore;
                    bestCol = col;
                }
                alpha = std::max(alpha, maxEval); 
                if (beta <= alpha)
                    break;
            }
        }
  //      std::cout << "EvalScore (if): " << maxEval << '\n';
        return { bestCol, 0, maxEval };
    }
    else {
        int minEval = 100000;
        int bestCol = -1;
        for (int col = 0; col < cols; ++col) {

            if (canPlace(board, col)) {
                auto boardCopy = copyBoard(board);

                auto lastMovement = makeMovement(boardCopy, col, playerLabel);
                int evalScore = minimax(boardCopy, depth - 1, alpha, beta, true).score;
                //undoLastMovement(board, lastMovement.row, lastMovement.col);
                if (evalScore < minEval) {
                    minEval = evalScore;
                    bestCol = col;
                }
                beta = std::min(beta, evalScore);
                if (beta <= alpha)
                    break;
            }
        }
       // std::cout << "MinEval: " << minEval << '\n';
        return { bestCol, 0, minEval };
    }
}

int main()
{
    setlocale(LC_ALL, "rus");
    std::vector<std::vector<char>> board(rows, std::vector<char>(cols,' '));
    std::cout << "Добро пожаловать в игру Четыре в ряд! \nДля победы вам нужно собрать 4 подряд фишки в столбце, строке или диагонали.\nВы будете ходить крестиками X, а я ноликами O! \nДавайте сыграем!\n";
    printBoard(board);
    int playersChoice;
    movement didntMove = { -1, -1, -100000 };
    movement playerMovement = { -1, -1, -100000 };
    movement aiMovement = { -1, -1, -100000 };
    while (true) {
        
        std::cout << "Введите номер столбца, куда хотите бросить фишку(0-6): ";
        std::cin >> playersChoice;
        if (playersChoice >= -1 && playersChoice <= 6) {
            std::cout << "playersChoice: " << playersChoice << '\n';

            if (playersChoice == -1 && playerMovement != didntMove && aiMovement != didntMove) {
                undoLastMovement(board, playerMovement.row, playerMovement.col);
                undoLastMovement(board, aiMovement.row, aiMovement.col);
                std::cout << "Вы отменили последний ход.\n";
                printBoard(board);
                continue;
            }
            if (canPlace(board, playersChoice)) {
                playerMovement = makeMovement(board, playersChoice, playerLabel);
                printBoard(board);
                if (checkWin(board, playerLabel)) {
                    std::cout << "Вы победили!\n";
                    return 0;
                }
                std::cout << "Ход компьютера...\n";
                auto bestMovementCol = minimax(board, maxDepth, -100000, 100000, true).col;
                aiMovement = makeMovement(board, bestMovementCol, aiLabel);
                std::cout << "Компьютер сделал ход в столбец " << bestMovementCol << "\n";
                printBoard(board);
                if (checkWin(board, aiLabel)) {
                    std::cout << "Вы проиграли!\n";
                    std::cout << "Желаете отменить последний ход? Для этого введите -1: ";
                    std::cin >> playersChoice;
                    if (playersChoice == -1) {
                        undoLastMovement(board, playerMovement.row, playerMovement.col);
                        undoLastMovement(board, aiMovement.row, aiMovement.col);
                        std::cout << "Вы отменили последний ход.\n";
                        printBoard(board);
                        continue;
                    }
                    return 0;
                }
            }
        }
        else {
            std::cout << "Некорректный или невозможный ход. Попробуйте ещё \n";
        }
        bool isDraw = true;
        for(auto col = 0; col<cols; ++col)
            if (canPlace(board, col)) {
                isDraw = false;
                break;
            }
        if (isDraw) {
            std::cout << "Ничья!\n";
            return 0;
        }

    }
}

