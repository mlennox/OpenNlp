using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using System.Data.Common;
using System.Data;

namespace SharpEntropy.IO
{
	/// <summary>
	/// Summary description for SqliteGisModelReader.
	/// </summary>
	public class SqliteGisModelReader : IGisModelReader
	{
        private SqliteConnection mDataConnection;
        private SqliteCommand mGetParameterCommand;
        private IDbDataParameter mPredicateParameter;

		private int mCorrectionConstant;
		private double mCorrectionParameter;
		private string[] mOutcomeLabels;

		public SqliteGisModelReader(string fileName)
		{
            mDataConnection = new SqliteConnection("Data Source=" + fileName + ";Compress=False;Synchronous=Off;UTF8Encoding=True;Version=3");
			mDataConnection.Open();

			SqliteCommand getModelCommand = mDataConnection.CreateCommand();
			getModelCommand.CommandText = "SELECT CorrectionConstant, CorrectionParameter FROM Model";
            using (DbDataReader reader = getModelCommand.ExecuteReader()) {
				while (reader.Read())
				{
					mCorrectionConstant = reader.GetInt32(0);
					mCorrectionParameter = reader.GetDouble(1);
				}
            }

            List<string> outcomeLabels = new List<string>();

			getModelCommand.CommandText = "SELECT OutcomeLabel FROM Outcome ORDER BY OutcomeID";
            using (DbDataReader reader = getModelCommand.ExecuteReader())
            {
				while (reader.Read())
				{
					outcomeLabels.Add(reader.GetString(0));
				}
            }
			
			mOutcomeLabels =outcomeLabels.ToArray();

			mGetParameterCommand = mDataConnection.CreateCommand();
			mGetParameterCommand.CommandText = "SELECT PredicateParameter.OutcomeID, PredicateParameter.Parameter FROM PredicateParameter INNER JOIN Predicate ON PredicateParameter.PredicateID = Predicate.PredicateID WHERE Predicate.PredicateLabel = ?";
            mPredicateParameter = new SqliteParameter();
            mPredicateParameter.DbType = DbType.String;
			mPredicateParameter.Size = 255;
            mGetParameterCommand.Parameters.Add(mPredicateParameter);
		}

		public int CorrectionConstant
		{
			get
			{
				return mCorrectionConstant;
			}
		}
	
		public double CorrectionParameter
		{
			get
			{
				return mCorrectionParameter;
			}
		}
	
		public string[] GetOutcomeLabels()
		{
			return mOutcomeLabels;
		}

		public int[][] GetOutcomePatterns()
		{
            throw new NotImplementedException();
		}

        public Dictionary<string, PatternedPredicate> GetPredicates()
		{
            throw new NotImplementedException();
		}

		public void GetPredicateData(string predicateLabel, int[] featureCounts, double[] outcomeSums)
		{
			mPredicateParameter.Value = predicateLabel;
            using (SqliteDataReader getParameterReader = mGetParameterCommand.ExecuteReader()) {
				int outcomeId;
				double parameter;
				while (getParameterReader.Read())
				{
					outcomeId = getParameterReader.GetInt32(0);
					parameter = getParameterReader.GetDouble(1);
					featureCounts[outcomeId]++;
					outcomeSums[outcomeId] += parameter;
				}
            }
		}
	}
}
