﻿using System;
using Keras.Applications;
using Numpy;
using Sudoku.Shared;
using System.Diagnostics;
using System.IO;

namespace Sudoku.CNN
{
    public class SolverKeras_CNN : PythonSolverBase
    {

		protected override void InitializePythonComponents()
		{
			InstallPipModule("tensorflow");
			InstallPipModule("keras");
			base.InitializePythonComponents();
		}

		public override SudokuGrid Solve(SudokuGrid s)
        {



            //string filepath = Path.Combine(Environment.CurrentDirectory, "variables");
            string filepath = "C:\\Users\\lolfi\\Documents\\GitHub\\MSMIN4IN32-22-MIN1-Sudoku\\Sudoku.CNN\\New_Model.h5";
            //if (!File.Exists(filepath))
            //{
				//File.WriteAllBytes(filepath, Resource1.variables);
			//}
			
			Keras.Models.BaseModel model = Keras.Models.Model.LoadModel(filepath);
            return Simple_solver(s, model);
        }

        private SudokuGrid Simple_solver(SudokuGrid s, Keras.Models.BaseModel model)
        {
            //Conversion de la SudokuGrid en array Numpy pour l'injection dans le CNN
            int[,] SudokuBuffer = new int[9,9];
            
            for (int i=0; i<9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    SudokuBuffer[i, j] = s.Cells[i][j];
                }
            } 
            

            //Prediction du CNN
            
            var prediction = np.array(SudokuBuffer);
            model.Predict(prediction);
            SudokuGrid sol = new SudokuGrid();
            
            //Reconversion de la solution en SudokuGrid
            for(int i = 0; i < 9; i++)
            {
                for (int j = 0; j<9; j++)
                {
                    sol.Cells[i][j] = (int)prediction[i][j];
                }
            }

            return sol;
        }

        
    }
}
