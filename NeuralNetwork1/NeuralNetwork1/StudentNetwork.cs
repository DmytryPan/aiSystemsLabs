using Accord;
using Accord.Statistics.Kernels;
using System;
using System.CodeDom;

namespace NeuralNetwork1
{
    public class StudentNetwork : BaseNetwork
    {
        // функция активации - сигмоида
        public static double Sigmoid(double x) => 1 / (1 + Math.Exp(-x));
        public static double DerivativeSigmoid(double outx) => outx * (1 - outx); // функция производной для сигмоиды. используем сразу выходной сигнал в качетве f(x)
        public static double learningRate = 0.01;
        private class Neuron
        {

            public Neuron[] inputs;
            //выходной сигнал
            public double output;
            //веса
            public double[] weights;
            //значение ошибки
            public double error;
            // биас
            public double bias;
            private Random rand = new Random();

            public Neuron(Neuron[] prevLayer = null)
            {
                if (prevLayer == null && prevLayer.Length == 0)
                    return;
                inputs = prevLayer;
                weights = new double[inputs.Length];
                InitRandomWeights(); // инициализация весов случайными значениями
            }

            public void Activate()
            {

                double Sum = 0;
                Sum += bias;
                for (int i = 0; i < inputs.Length; ++i)
                {
                    Sum += inputs[i].output * weights[i]; // взвешенная сумма сигналов
                }
                output = Sigmoid(Sum);
            }
            // Инициализация весов случайными величинами
            private void InitRandomWeights()
            {
                for (int i = 0; i < weights.Length; i++)
                {
                    weights[i] = rand.NextDouble();
                }
                bias = rand.NextDouble();
            }

            internal void AdjWeights()
            {
                for (int i = 0;  i < weights.Length; ++i)
                {
                    weights[i] -= learningRate * error * inputs[i].output;
                }
                bias -= learningRate * error;
            }
        }
        Neuron[] sensors;
        Neuron[] outputs;
        Neuron[][] layers;

        public StudentNetwork(int[] structure)
        {
            InitNetwork(structure);
        }

        private void InitNetwork(int[] structure)
        {
            if (structure.Length < 2)
            {
                throw new ArgumentException("invalid structure network");
            }
            layers = new Neuron[structure.Length][];
            // выделяем память под сенсорный слой и его нейроны
            layers[0] = new Neuron[structure[0]];
            for (int i = 0;i < structure[0]; i++)
            {
                layers[0][i] = new Neuron(); 
            }
            //выделяем память под остальные слои
            for(int i = 1; i < structure.Length; i++)
            {
                layers[i] = new Neuron[structure[i]];
                for (int j = 0; j < structure[i]; j++)
                {
                    layers[i][j] = new Neuron(layers[i - 1]); // каждый нейрон связан с нейроном предыдущего слоя
                }
            }
            // присваиваем ссылки 
            sensors = layers[0];
            outputs = layers[layers.Length-1];
        }

        public override int Train(Sample sample, double acceptableError, bool parallel)
        {
            throw new NotImplementedException();
        }

        public override double TrainOnDataSet(SamplesSet samplesSet, int epochsCount, double acceptableError, bool parallel)
        {
            throw new NotImplementedException();
        }

        protected override double[] Compute(double[] input)
        {
            throw new NotImplementedException();
        }

        // обратное распространение ошибки
        private void BackProp(Sample image)
        {
            //ошибка выходного слоя
            for (int i = 0; i < outputs.Length; i++)
            {
                outputs[i].error = image.error[i];
            }

            // ошибки скрытых слоев
            // перебор слоев
            for (int layer = layers.Length - 2; layer > 0; layer--)
            {
                // перебор нейронов слоя layer
                for (int j = 0; j < layers[layer].Length; j++)
                {
                    double sum = 0;
                    // перебор нейронов следующего слоя  за layer
                    for (int o = 0; o < layers[layer + 1].Length; o++)
                    {
                        sum += layers[layer + 1][o].error * layers[layer + 1][o].weights[j];
                    }
                    layers[layer][j].error = sum * DerivativeSigmoid(layers[layer][j].output);
                }
            }
            // обновление весов выходного слоя
            for (int i = 0; i < outputs.Length; ++i)
            {
                outputs[i].AdjWeights();
            }
            // обновление весов нейронов скрытых слове
            for (int layer = layers.Length - 2; layer > 0; layer--)
            {
                for (int j = 0; j < layers[layer].Length; ++j)
                {
                    layers[layer][j].AdjWeights();
                }
            }
        }
    }
}