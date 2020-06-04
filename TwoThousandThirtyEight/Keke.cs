using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Reflection.Emit;

namespace TwpThousanFirthyEight
{

    public class Many
    {
        public Random random = new Random();

        public List<One> all_ones = new List<One>();
        public bool HeIsAlreadyWon()
        {
            foreach (var item in all_ones)
            {
                if (item.Value == 2048)
                {
                    return true;
                }
            }           
            return false;
        }
        public Many()
        {
          
            for (int i = 0; i < 16; i++)
            {
                all_ones.Add(new One());
            }
            int ind = random.Next(16);
            all_ones[ind].Value = 2;
            return;
        }
        public int New()//создание нового элемента со случ значением(2 или 4)
        {
            One empty = new One();
            int four_or_two = random.Next(11);
           
            if (all_ones.Contains(empty))
            {
                while (true)
                {
                    int ind = random.Next(16);
                    if ((all_ones[ind].Value == -1) && (four_or_two == 9))
                    {
                        all_ones[ind].Value = 4;
                        return ind;
                    }
                    else if ((all_ones[ind].Value == -1) && (four_or_two != 9))
                    {
                        all_ones[ind].Value = 2;
                        return ind;
                    }
                   
                }
            }
            else { return -1; }
            
        }
        public void New(int val)//создание элемента случайного со значением 2, необходимо для первого хода
        {
           
            while (true)
            {
                int ind = random.Next(16);
                if ((all_ones[ind].Value == -1) && (val == 2))
                {
                    all_ones[ind].Value = 2;
                    return;
                }


            }
        }
        public void Clean()//обнуляет массив, необходимо для Retry
        {

            all_ones.Clear();       
            
        }
        private void IsNotSum()//процедура, необходимая для движений
        {
            foreach (var item in all_ones)
            {
                item.IsAlreadySum = false;
            }
        }
        public bool MovingUp()//вверх
        {
            int[] cicle = new int[] { 0, 4, 8, 12, 1, 5, 9, 13, 2, 6, 10, 14, 3, 7, 11, 15 };
            List<One> old = new List<One>();
            foreach (var item in all_ones)
            {
                old.Add((One)item.Clone());
            }
            foreach (var i in cicle)
            {
                if (all_ones[i].Value != -1)
                {
                    if (i - 4 >= 0)
                    {
                        if (all_ones[i - 4].Value == -1)
                        {
                            all_ones[i - 4].Value = all_ones[i].Value;
                            all_ones[i].Value = -1;
                            if (i - 8 >= 0)
                            {
                                if (all_ones[i - 8].Value == -1)
                                {
                                    all_ones[i - 8].Value = all_ones[i - 4].Value;
                                    all_ones[i - 4].Value = -1;
                                    if (i - 12 >= 0)
                                    {
                                        if (all_ones[i - 12].Value == -1)
                                        {
                                            all_ones[i - 12].Value = all_ones[i - 8].Value;
                                            all_ones[i - 8].Value = -1;
                                        }
                                        else if ((all_ones[i - 12].Value == all_ones[i - 8].Value) && (all_ones[i - 8].Value > 0)&&(!all_ones[i - 12].IsAlreadySum))
                                        {
                                            all_ones[i - 12].Value *= 2;
                                            all_ones[i - 12].IsAlreadySum = true;
                                            all_ones[i - 8].Value = -1;
                                            all_ones[i - 8].IsAlreadySum = false;
                                        }
                                    }

                                }
                                else if ((all_ones[i - 8].Value == all_ones[i - 4].Value) && (all_ones[i - 4].Value > 0)&&(!all_ones[i-8].IsAlreadySum))
                                {
                                    all_ones[i - 8].Value *= 2;
                                    all_ones[i - 8].IsAlreadySum = true;
                                    all_ones[i - 4].Value = -1;
                                    all_ones[i - 4].IsAlreadySum = false;

                                }
                            }
                        }
                        else if ((all_ones[i - 4].Value == all_ones[i].Value) && (all_ones[i].Value > 0)&&(!all_ones[i-4].IsAlreadySum))
                        {
                            all_ones[i - 4].Value *= 2;
                            all_ones[i - 4].IsAlreadySum = true;
                            all_ones[i].Value = -1;
                            all_ones[i].IsAlreadySum = false;
                        }
                    }
                }
            }
            IsNotSum();
            return (!all_ones.SequenceEqual(old));
        }
        public bool HeIsAlreadyDead()//проверка на проигрыш
        {
            bool[] prov = new bool[16];
            for (int i = 0; i < prov.Length; i++)
            {
                prov[i] = false;
            }
            for (int i = 0; i < all_ones.Count; i++)
            {
                if (all_ones[i].Value != -1)
                {
                    if (i == 0)
                    {
                        if ((all_ones[i + 1].Value != all_ones[i].Value) && (all_ones[i + 4].Value != all_ones[i].Value)
                            && (all_ones[i +1].Value != -1) && (all_ones[i + 4].Value != -1))
                        {
                            prov[i] = true;
                        }
                    }
                    else if (i == 3)
                    {
                        if ((all_ones[i - 1].Value != all_ones[i].Value) && (all_ones[i + 4].Value != all_ones[i].Value)
                            && (all_ones[i - 1].Value != -1) && (all_ones[i + 4].Value != -1))
                        {
                            prov[i] = true;
                        }

                    }
                    else if (i == 15)
                    {
                        if ((all_ones[i - 4].Value != all_ones[i].Value) && (all_ones[i - 1].Value != all_ones[i].Value)
                            && (all_ones[i - 4].Value != -1) && (all_ones[i - 1].Value != -1))
                        {
                            prov[i] = true;
                        }
                    }
                    else if (i == 12)
                    {
                        if ((all_ones[i - 4].Value != all_ones[i].Value) && (all_ones[i + 1].Value != all_ones[i].Value)
                             && (all_ones[i - 4].Value != -1) && (all_ones[i + 1].Value != -1))
                        {
                            prov[i] = true;
                        }
                    }
                    else if ((i == 4) || (i == 8))
                    {
                        if ((all_ones[i - 4].Value != all_ones[i].Value) && (all_ones[i + 1].Value != all_ones[i].Value) && (all_ones[i + 4].Value != all_ones[i].Value)
                             && (all_ones[i - 4].Value != -1) && (all_ones[i + 1].Value != -1) && (all_ones[i + 4].Value != -1))
                        {
                            prov[i] = true;
                        }
                    }
                    else if ((i == 7) || (i == 11))
                    {
                        if ((all_ones[i - 4].Value != all_ones[i].Value) && (all_ones[i - 1].Value != all_ones[i].Value) && (all_ones[i + 4].Value != all_ones[i].Value)
                             && (all_ones[i - 4].Value != -1) && (all_ones[i - 1].Value != -1) && (all_ones[i + 4].Value != -1))
                        {
                            prov[i] = true;
                        }
                    } else if ((i == 1) || (i == 2))
                    {
                        if ((all_ones[i + 4].Value != all_ones[i].Value) && (all_ones[i - 1].Value != all_ones[i].Value) && (all_ones[i + 1].Value != all_ones[i].Value)
                             && (all_ones[i + 4].Value != -1) && (all_ones[i - 1].Value != -1) && (all_ones[i + 1].Value != -1))
                        {
                            prov[i] = true;
                        }
                    } else if ((i == 13) || (i == 14))
                    {
                        if ((all_ones[i - 4].Value != all_ones[i].Value) && (all_ones[i - 1].Value != all_ones[i].Value) && (all_ones[i + 1].Value != all_ones[i].Value)
                            && (all_ones[i - 4].Value != -1) && (all_ones[i - 1].Value != -1) && (all_ones[i + 1].Value != -1))
                        {
                            prov[i] = true;
                        }

                    }
                    else
                    {
                        if ((all_ones[i - 4].Value != all_ones[i].Value) && (all_ones[i - 1].Value != all_ones[i].Value) && (all_ones[i + 1].Value != all_ones[i].Value)
                            &&(all_ones[i + 4].Value != all_ones[i].Value)&&(all_ones[i - 4].Value !=-1)&&(all_ones[i - 1].Value != -1) && (all_ones[i + 4].Value != -1) && (all_ones[i + 1].Value != -1))
                        {
                            prov[i] = true;
                        }
                    }

                }
            }
          
            return prov.All(value => value == true);

        }
        public bool MovingDown()//вниз
        {
            int[] cicle = new int[] { 12, 8, 4, 0, 13, 9, 5, 1, 14, 10, 6, 2, 15, 11, 7, 3 };
            List<One> old = new List<One>();
        
            foreach (var item in all_ones)
            {
                old.Add((One)item.Clone());
            }
            foreach (var i in cicle)
            {
                if (all_ones[i].Value != -1)
                {
                    if (i + 4 <= 15)
                    {
                        if (all_ones[i + 4].Value == -1)
                        {
                            all_ones[i + 4].Value = all_ones[i].Value;
                            all_ones[i].Value = -1;
                            if (i + 8 <= 15)
                            {
                                if (all_ones[i + 8].Value == -1)
                                {
                                    all_ones[i + 8].Value = all_ones[i + 4].Value;
                                    all_ones[i + 4].Value = -1;
                                    if (i + 12 <= 15)
                                    {
                                        if (all_ones[i + 12].Value == -1)
                                        {
                                            all_ones[i + 12].Value = all_ones[i + 8].Value;
                                            all_ones[i + 8].Value = -1;
                                        }
                                        else if ((all_ones[i + 12].Value == all_ones[i + 8].Value) && (all_ones[i + 8].Value > 0) && (!all_ones[i +12].IsAlreadySum))
                                        {
                                            all_ones[i + 12].Value *= 2;
                                            all_ones[i + 12].IsAlreadySum = true;
                                            all_ones[i + 8].Value = -1;
                                            all_ones[i + 8].IsAlreadySum = false;
                                        }
                                    }

                                }
                                else if ((all_ones[i + 8].Value == all_ones[i + 4].Value) && (all_ones[i + 4].Value > 0) && (!all_ones[i + 8].IsAlreadySum))
                                {
                                    all_ones[i + 8].Value *= 2;
                                    all_ones[i +8].IsAlreadySum = true;
                                    all_ones[i + 4].Value = -1;
                                    all_ones[i + 4].IsAlreadySum = false;
                                }
                            }
                        }
                        else if ((all_ones[i + 4].Value == all_ones[i].Value) && (all_ones[i].Value > 0)&& (!all_ones[i + 4].IsAlreadySum))
                        {
                            all_ones[i + 4].Value *= 2;
                            all_ones[i + 4].IsAlreadySum = true;
                            all_ones[i].Value = -1;
                            all_ones[i].IsAlreadySum = false;
                        }
                    }
                }
            }
            IsNotSum();
            return (!all_ones.SequenceEqual(old));
        }
        public bool MovingLeft()//влево
        {
            int[] cicle = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            List<One> old = new List<One>();
         
            foreach (var item in all_ones)
            {
                old.Add((One)item.Clone());
            }
            foreach (var i in cicle)
            {
                if (all_ones[i].Value != -1)
                {
                    if ((i != 0) && (i != 4) && (i != 8) && (i != 12))
                    {
                        if (all_ones[i - 1].Value == -1)
                        {
                            all_ones[i - 1].Value = all_ones[i].Value;
                            all_ones[i].Value = -1;
                            if ((i - 1 != 0) && (i - 1 != 4) && (i - 1 != 8) && (i - 1 != 12))
                            {
                                if (all_ones[i - 2].Value == -1)
                                {
                                    all_ones[i - 2].Value = all_ones[i - 1].Value;
                                    all_ones[i - 1].Value = -1;
                                    if ((i - 2 != 0) && (i - 2 != 4) && (i - 2 != 8) && (i - 2 != 12))
                                    {
                                        if (all_ones[i - 3].Value == -1)
                                        {
                                            all_ones[i - 3].Value = all_ones[i - 2].Value;
                                            all_ones[i - 2].Value = -1;
                                        }
                                        else if ((all_ones[i - 3].Value == all_ones[i - 2].Value) && (all_ones[i - 2].Value > 0) && (!all_ones[i - 3].IsAlreadySum))//?
                                        {
                                            all_ones[i - 3].Value *= 2;
                                            all_ones[i - 3].IsAlreadySum = true;
                                            all_ones[i - 2].Value = -1;
                                            all_ones[i - 2].IsAlreadySum = false;
                                        }
                                    }

                                }
                                else if ((all_ones[i - 2].Value == all_ones[i - 1].Value) && (all_ones[i - 1].Value > 0) && (!all_ones[i - 2].IsAlreadySum))
                                {
                                    all_ones[i - 2].Value *= 2;
                                    all_ones[i - 2].IsAlreadySum = true;
                                    all_ones[i - 1].Value = -1;
                                    all_ones[i - 1].IsAlreadySum = false;
                                }
                            }
                        }
                        else if ((all_ones[i - 1].Value == all_ones[i].Value) && (all_ones[i].Value > 0) && (!all_ones[i].IsAlreadySum))
                        {
                            all_ones[i - 1].Value *= 2;
                            all_ones[i - 1].IsAlreadySum = true;
                            all_ones[i].Value = -1;
                            all_ones[i].IsAlreadySum = false;
                        }
                    }


                }
               
            }
            IsNotSum();
            return (!all_ones.SequenceEqual(old));
        }   
        public bool MovingRight()//что собственно происходит с массивом, когда двигаю вправо
        {
            int[] cicle = new int[] { 15,14,13,12,11,10,9,8,7,6,5,4,3,2,1,0 };
            List<One> old = new List<One>();
            foreach (var item in all_ones)
            {
                old.Add((One)item.Clone());
            }
            foreach (var i in cicle)
            {
                if (all_ones[i].Value != -1)
                {
                    if ((i != 3) && (i != 7) && (i != 11) && (i != 15))
                    {
                        if (all_ones[i + 1].Value == -1)
                        {
                            all_ones[i + 1].Value = all_ones[i].Value;
                            all_ones[i].Value = -1;
                            if ((i + 1 != 3) && (i + 1 != 7) && (i + 1 != 11) && (i + 1 != 15))
                            {
                                if (all_ones[i + 2].Value == -1)
                                {
                                    all_ones[i + 2].Value = all_ones[i + 1].Value;
                                    all_ones[i + 1].Value = -1;
                                    if ((i + 2 != 3) && (i + 2 != 7) && (i + 2 != 11) && (i + 2 != 15))
                                    {
                                        if (all_ones[i + 3].Value == -1)
                                        {
                                            all_ones[i + 3].Value = all_ones[i + 2].Value;
                                            all_ones[i + 2].Value = -1;
                                        }
                                        else if ((all_ones[i + 3].Value == all_ones[i + 2].Value) && (all_ones[i + 2].Value > 0) && (!all_ones[i+3].IsAlreadySum))
                                        {
                                            all_ones[i + 3].Value *= 2;
                                            all_ones[i + 3].IsAlreadySum = true;
                                            all_ones[i + 2].Value = -1;
                                            all_ones[i+2].IsAlreadySum = false;
                                        }
                                    }

                                }
                                else if ((all_ones[i + 2].Value == all_ones[i + 1].Value) && (all_ones[i + 1].Value > 0) && (!all_ones[i + 2].IsAlreadySum))
                                {
                                    all_ones[i + 2].Value *= 2;
                                    all_ones[i + 2].IsAlreadySum = true;
                                    all_ones[i + 1].Value = -1;
                                    all_ones[i+1].IsAlreadySum = false;
                                }
                            }
                        }
                        else if ((all_ones[i + 1].Value == all_ones[i].Value) && (all_ones[i].Value > 0)&&(!all_ones[i].IsAlreadySum))
                        {
                            all_ones[i + 1].Value *= 2;
                            all_ones[i + 1].IsAlreadySum = true;
                            all_ones[i].Value = -1;
                            all_ones[i].IsAlreadySum = false;
                        }
                    }


                }
            }
            IsNotSum();
            return (!all_ones.SequenceEqual(old));
        }   
    }

    public class One: IEquatable<One>,ICloneable
    {
       
        private int value;
        private bool isAlreadySum;
        
        public int Value { get { return this.value; } set { this.value = value; } }

        public bool IsAlreadySum { get { return this.isAlreadySum; } set { this.isAlreadySum = value; } }

        public One()
        {
            value = -1;
            isAlreadySum = false;
        }
        public override string ToString()
        {
            return ( "Value=" + this.Value);
        }

        public bool Equals(One obj)
        {
            return (obj.Value == this.Value);
        }
        public object Clone()
        {
            return new One() { IsAlreadySum = this.IsAlreadySum, Value = this.Value };
        }



    }
}
