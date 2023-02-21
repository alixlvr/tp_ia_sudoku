﻿using System;
using System.Diagnostics;
using aima.core.search.csp;
using Sudoku.Shared;


namespace Sudoku.CSPSolver
{
    public abstract class CSPSolverBase:ISudokuSolver
    {

	    public CSPSolverBase()
	    {
		    _Strategy = GetStrategy();
	    }

        private readonly SolutionStrategy _Strategy;

		// Vous n'avez plus qu'à suivre les commentaires, ça devrait aller vite.
      public SudokuGrid Solve(SudokuGrid s)
        {
			//Construction du CSP en utilisant CspHelper
			var objCSp =SudokuCSPHelper.GetSudokuCSP(s);

			// Utilisation de la stratégie pour résoudre le CSP
			var assignment = _Strategy.solve(objCSp);

			//Utilisation de CSPHelper pour traduire l'assignation en SudokuGrid
			SudokuCSPHelper.SetValuesFromAssignment(assignment,s);

			return s;
		}

		protected abstract SolutionStrategy GetStrategy();

	}

	//Voilà un premier solver, je vous laisse implémenter les autres avec différentes combinaisons de paramètres pour pouvoir contraster leurs performances
    public class CSPMRVDegLCVFCSolver : CSPSolverBase
    {
	    protected override SolutionStrategy GetStrategy()
	    {
			var objStrategyInfo = new CSPStrategyInfo
			{
				EnableLCV = true,
				Inference = CSPInference.ForwardChecking,
				Selection = CSPSelection.MRVDeg,
				StrategyType = CSPStrategy.ImprovedBacktrackingStrategy,
				MaxSteps = 5000
			};
			return  objStrategyInfo.GetStrategy();
		}
    }

	//Voilà un deuxième solver. Il existe encore plein d'autres combinaisons possibles, je vous laisse le soin de proposer tous les solvers possibles de façon à en comparer les performances.
	//Le paramètre MaxSteps concerne la stratégie MinConflicts
	public class CSPMRVAC3Solver : CSPSolverBase
    {
	    protected override SolutionStrategy GetStrategy()
	    {
		    var objStrategyInfo = new CSPStrategyInfo
		    {
			    EnableLCV = false,
			    Inference = CSPInference.AC3,
			    Selection = CSPSelection.MRV,
			    StrategyType = CSPStrategy.ImprovedBacktrackingStrategy,
			    MaxSteps = 5000
		    };
		    return objStrategyInfo.GetStrategy();
	    }
    }


}
