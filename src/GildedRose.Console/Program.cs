using System.Collections.Generic;

namespace GildedRose.Console
{
    public class Program
    {
        public IList<Item> Items;
        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program()
                          {
                              Items = new List<Item>
                                          {
                                              new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                                              new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                                              new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                                              new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                                              new Item
                                                  {
                                                      Name = "Backstage passes to a TAFKAL80ETC concert",
                                                      SellIn = 15,
                                                      Quality = 20
                                                  },
                                              new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                                          }

                          };

            app.UpdateQuality();

            System.Console.ReadKey();

        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                var item = Items[i];
                UpdateItem(item);
            }
        }

        private static void UpdateItem(Item item)
        {
            var cfactor = 1;
            if (item.Name.ToLower().Contains("conjured"))
            {
                cfactor = 2;
            }
            if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
            {
                if (item.Quality < 50)
                {
                    item.Quality = item.Quality + 1*cfactor;


                    if (item.SellIn < 11)
                    {
                        if (item.Quality < 50)
                        {
                            item.Quality = item.Quality + 1*cfactor;
                        }
                    }

                    if (item.SellIn < 6)
                    {
                        if (item.Quality < 50)
                        {
                            item.Quality = item.Quality + 1*cfactor;
                        }
                    }
                }


                item.SellIn = item.SellIn - 1;


                if (item.SellIn < 0)
                {
                    item.Quality = item.Quality - item.Quality;
                }
                return;
            }
            else if (item.Name == "Aged Brie")
            {
                if (item.Quality < 50)
                {
                    item.Quality = item.Quality + 1*cfactor;
                }

                item.SellIn = item.SellIn - 1;

                if (item.SellIn < 0)
                {
                    if (item.Quality < 50)
                    {
                        item.Quality = item.Quality + 1*cfactor;
                    }
                }
                return;
            }
            else if (item.Name == "Sulfuras, Hand of Ragnaros")
            {
                return;
            }
            else
            {
                if (item.Quality > 0)
                {

                    item.Quality = item.Quality - 1*cfactor;

                }
            }


            item.SellIn = item.SellIn - 1;


            if (item.SellIn < 0 )
            {
                if (item.Quality > 0)
                {
                    item.Quality = item.Quality - 1*cfactor;
                }
            }
        }
    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

}
