﻿using System;
using SkiaSharp;

namespace DAT190_Bachelor_Project.Model
{
    public interface IEmission
    {
        // Properties
        int Id { get; set; }
        double KgCO2 { get; set; }
        DateTime Date { get; set; }
        EmissionType Type { get; set; }

        // Methods
        double CalculateCO2(double amount);
        string BiggestEmissionFactorDescription();
        string SimplestEmissionReductionMeasure();

    }
}
