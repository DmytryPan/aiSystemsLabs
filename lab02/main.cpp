
#include <iostream>
#include <vector>
#include <string>
#include "algs.h"


void DoBeforeAlgs()
{
    endMask = 0;
    for (uint64_t i = 0; i < 16; ++i)
    {
        auto val = (i + 1) % 16;
        posFinal[i] = val;
        posMask[i] = 0x0FULL << i * 4;
        posCols[val] = i % 4;
        posRows[val] = i / 4;
        endMask += val << i * 4;

        for (int j = 0; j < 16; ++j) {
            int element = val;
            if (element == 0)continue;
            int x1 = j % 4;
            int y1 = j / 4;
            int x2 = posCols[element];
            int y2 = posRows[element];

            mDist[element][j] = abs(x1 - x2) + abs(y1 - y2);
        }
    }
    /*
    for (int i = 1; i < 16; ++i)
    {
        cout << i << ": " << endl;
        for (int y = 0; y < 4; ++y)
        {
            for (int x = 0; x < 4; ++x)
            {
               cout << mDist[i][x + y * 4] << " ";
            }
            cout << endl;
        }
        cout << endl;
    }
    cout << endl;*/
}

int main()
{
    setlocale(LC_ALL, "ru");
    DoBeforeAlgs();

    std::vector<std::string> input_positions{ "F2345678A0BE91CD", "123456789AFB0EDC", "123456789ABCDEF0", "1234067859ACDEBF",
        "5134207896ACDEBF", "16245A3709C8DEBF", "1723068459ACDEBF", "12345678A0BE9FCD",
        "51247308A6BE9FCD", "F2345678A0BE91DC", "75123804A6BE9FCD", "75AB2C416D389F0E",
        "04582E1DF79BCA36", "FE169B4C0A73D852", "D79F2E8A45106C3B", "DBE87A2C91F65034",
        "BAC0F478E19623D5" };
    
    /*fillField("5134207896ACDEBF", field);
    bfs();
    fillField("16245A3709C8DEBF", field);
    bfs();*/

    //
    for (auto& pos : input_positions) {
        if (is_solvable(pos)) {
            //std::cout << pos << " " << "разрешима! \n"; 
            fillField(pos, field);
            //bfs();
            //Astar();
            //IDS();
            IDAstar();
            //print(fieldInt);
            
        }
        else
            std::cout << pos << " " << "не разрешима!\n";
    }
    //std::cout << '\n';
    //fillField("16245A3709C8DEBF", field);
    //print(fieldInt);

}

