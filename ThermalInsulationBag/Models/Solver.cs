using OxyPlot;
using System;
using System.Collections.Generic;

namespace ThermalInsulationBag.Models
{
    public class Solver
    {
        static private List<DataPoint> tempPts;
        static private List<DataPoint> lossPts;

        static public List<DataPoint> TempPts { get => tempPts; }
        static public List<DataPoint> LossPts { get => lossPts; }

        static private float OuterSquare(float lenght, float width, float height)
        {
            return 2 * (lenght * width + width * height + height * lenght);
        }

        static private float HeatTransferCoefficient(double widthMaterial, float innerCoef, float outerCoef, double thermalConductivity)
        {

            return (float)Math.Pow(1 / innerCoef + widthMaterial / thermalConductivity + 1 / outerCoef, -1);
        }

        static public float ThermalLoss(InputParams inputParams, MaterialInfo material)
        {
            return OuterSquare(inputParams.Length, inputParams.Width, inputParams.Height) * HeatTransferCoefficient(material.Width / 1000, inputParams.Coef1, inputParams.Coef2, material.ThermalConductivity) *
                (inputParams.TempIn - inputParams.TempOut);
        }

        static private float BagVolume(InputParams input)
        {
            return input.Length * input.Width * input.Height;
        }

        static private float BagContentVolume(InputParams input)
        {
            return input.BagFilling * BagVolume(input);
        }

        static private float BagContentMass(InputParams input)
        {
            return input.BagDensity * BagContentVolume(input);
        }

        static private float ThermalLoss(InputParams inputParams, MaterialInfo material, float tempPrev)
        {
            return OuterSquare(inputParams.Length, inputParams.Width, inputParams.Height) * HeatTransferCoefficient(material.Width / 1000, inputParams.Coef1, inputParams.Coef2, material.ThermalConductivity) *
                (tempPrev - inputParams.TempOut) * inputParams.TimeStep;
        }

        static public float TimeCooling(InputParams input, MaterialInfo material)
        {
            float Tn = input.TempIn;
            int n = 0;
            tempPts = new List<DataPoint>();
            lossPts = new List<DataPoint>();
            while (Tn >= input.FinalTemp)
            {
                var loss = ThermalLoss(input, material, Tn);
                Tn -= (loss - input.PowerHeatElement) / (input.HeatCapacityInBag * BagContentMass(input));
                try
                {
                    tempPts.Add(new DataPoint(n * input.TimeStep / 3600, Tn));
                    lossPts.Add(new DataPoint(n * input.TimeStep / 3600, loss / input.TimeStep));
                }
                catch
                {
                    break;
                }
                n++;
            }
            return n * input.TimeStep;
        }
    }
}
