using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace ShadowsOfTheKnight
{
	public class Bomb
	{
		private int row; private int column; private int floor;
		public int Row
		{
			get{ return this.row; }
		}

		public int Column
		{
			get{ return this.column; }
		}

		public int Floor
		{
			get{ return this.floor; }
		}

		public Bomb(int row, int column, int floor)
		{
			this.row = row;
			this.column = column;
			this.floor = floor;
		}

		public override String ToString(){
			return this.row.ToString()+","+this.column.ToString()+","+this.floor.ToString();
		}

	}

	public class Program
	{
		static Bomb[] bombList;

		public static void Main(string[] args)
		{
			Console.WriteLine("Enter the number of grid rows : ");
			int nuberOfGridRows = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine("Enter the number of grid columns : ");
			int nuberOfGridColumns = Convert.ToInt32(Console.ReadLine());
			Console.WriteLine("Enter the maximum number of building floors : ");
			int maxFloor = Convert.ToInt32(Console.ReadLine())+1;
			Console.WriteLine("Enter the number of bombs : ");
			int numberOfBomb = Convert.ToInt32(Console.ReadLine());

			ArrayList[,] grid = new ArrayList[nuberOfGridRows, nuberOfGridColumns];

			bombList = new Bomb[numberOfBomb];

			Random random = new Random();

			for (int i= 0; numberOfBomb > i; i++)
			{
				int bombRow = random.Next(nuberOfGridRows);
				int bombColumn = random.Next(nuberOfGridColumns);
				int bombFloor = random.Next(maxFloor);

				Bomb bomb = new Bomb(bombRow, bombColumn, bombFloor);

				bombList[i] = bomb;
			}

			//Grid oluşturuluyor.
			for(int i=0; nuberOfGridRows > i; i++)
			{
				for(int j = 0; nuberOfGridColumns > j; j++)
				{
					//Random kat sayısı belirle
					int floor = random.Next(maxFloor);

					//Bombalardan birinin bulunduğu konuma geldiysem kat sayısı en az bombanın bulunduğu kat sayısı kadar olmalı.
					int max = -1;
					for(int l = 0;numberOfBomb>l;l++){
						if(bombList[l].Row == i && bombList[l].Column == j){
							if(bombList[l].Floor>max){
								max = bombList[l].Floor;
							}
						}
					}

					//Bomba bu gridde. Kat sayısını ayarla.
					if(max!=-1){
						floor = random.Next(max,maxFloor);
					}

					Console.WriteLine(i.ToString()+","+j.ToString() + " number of floor :  " + floor );

					ArrayList building = new ArrayList();

					double totalDistance = 0;
					for (int k=0; maxFloor > k; k++)
					{
						if (floor >= k)
						{
							double distance = calculateDistance(i, j, k);
							totalDistance+=distance;
							building.Add(distance);

							Console.WriteLine(distance);
						}
						else{
							break;
						}
					}

					grid[i, j] = building;

					Console.WriteLine();

				}
			}
			
			for(int i=0; nuberOfGridRows>i;i++){
				for(int j=0;nuberOfGridColumns>j;j++){
					ArrayList arrayList = grid[i, j];
					for(int k=0; arrayList.Count>k;k++){
						if((double)arrayList[k]>1){
							Console.WriteLine("Bomb finded:"+i.ToString()+","+j.ToString()+","+k.ToString());
						}
					}
				}
			}

			Console.ReadLine();

		}

		private static double calculateDistance(int row, int column, int floor)
		{
			double totalDistance = 0;
			for(int i = 0; bombList.Length>i;i++){
				double distance = Math.Sqrt((Math.Pow(bombList[i].Row - row, 2) + Math.Pow(bombList[i].Column - column, 2) + Math.Pow(bombList[i].Floor - floor, 2)));
				if(distance == 0){
					totalDistance = 0;
					break;
				}
				else{
					totalDistance += distance;
				}
			}
			if (totalDistance != 0)
			{
				return 1 / totalDistance;
			}
			else
			{
				return Double.MaxValue;
			}
		}
	}
}
