using System.Collections.Generic;
using System.Configuration;
using GildedRose.Console;
using Xunit;
using Xunit.Sdk;

namespace GildedRose.Tests
{
    public class ProgramTests
    {
        private readonly Program program;
     
        public ProgramTests()
        {
            program = new Program();
        }

        private void FeedProgramWith(Item item)
        {
            program.Items = new List<Item>() { item };
        }

        [Fact]
        public void Once_the_sell_by_date_has_passed_Quality_degrades_twice_as_fast()
        {
            FeedProgramWith(new Item {Name = "Expired Item", SellIn = -1, Quality = 20});
            AssertOnItem(-1, 20);

            program.UpdateQuality();

            AssertOnItem(-2, 18);
        }

        [Fact]
        public void The_Quality_of_an_item_is_never_negative()
        {
            FeedProgramWith(new Item {Name = "Zero Quality Item", SellIn = -1, Quality = 0});
            AssertOnItem(-1, 0);

            program.UpdateQuality();

            AssertOnItem(-2, 0);
        }

        [Fact]
        public void Aged_Brie_actually_increases_in_Quality_the_older_it_gets()
        {
            FeedProgramWith(new Item { Name = "Aged Brie", SellIn = 10, Quality = 2 });
            AssertOnItem(10, 2);

            program.UpdateQuality();

            AssertOnItem(9, 3);
        }

        [Fact]
        public void The_Quality_of_an_item_is_never_more_than_50()
        {
            FeedProgramWith(new Item { Name = "Aged Brie", SellIn = 10, Quality = 50 });
            AssertOnItem(10, 50);

            program.UpdateQuality();

            AssertOnItem(9, 50);
        }

        [Fact]
        public void Expired_Aged_Brie_actually_increases_in_Quality_the_older_it_gets()
        {
            FeedProgramWith(new Item { Name = "Aged Brie", SellIn = 0, Quality = 30 });

            program.UpdateQuality();

            AssertOnItem(-1, 32);
        }

        [Fact]
        public void Sulfuras_being_a_legendary_item_never_has_to_be_sold_or_decreases_in_Quality()
        {
            FeedProgramWith(new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 10, Quality = 50 });
            AssertOnItem(10, 50);

            program.UpdateQuality();

            AssertOnItem(10, 50);
        }

        [Fact]
        public void Backstage_passes_like_aged_brie_increases_in_Quality_as_its_SellIn_value_approaches()
        {
            FeedProgramWith(new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 11, Quality = 10 });
            AssertOnItem(11, 10);

            program.UpdateQuality();
            AssertOnItem(10, 11);
        }

        [Fact]
        public void Backstage_passes_Quality_increases_by_2_when_there_are_10_days_or_less()
        {
            FeedProgramWith(new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 10, Quality = 10 });

            AssertOnItem(10, 10);
            program.UpdateQuality();
            AssertOnItem(9, 12);
        }

        [Fact]
        public void Backstage_passes_Quality_increases_by_3_when_there_are_5_days_or_less()
        {
            FeedProgramWith(new Item {Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 5, Quality = 10});
            AssertOnItem(5, 10);

            program.UpdateQuality();

            AssertOnItem(4, 13);
        }

        [Fact]
        public void Backstage_passes_Quality_drops_to_0_after_the_concert()
        {
            FeedProgramWith(new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 0, Quality = 10 });
            AssertOnItem(0, 10);

            program.UpdateQuality();

            AssertOnItem(-1, 0);
        }

        [Fact]
        public void Conjured_items_degrade_in_Quality_twice_as_fast_as_normal_items()
        {
            FeedProgramWith(new Item { Name = "Conjured Mana Cake", SellIn = 5, Quality = 10 });

            program.UpdateQuality();

            AssertOnItem(4, 8);
        }

        [Fact]

        public void Expired_Conjured_items_degrade_in_Quality_twice_as_fast_as_normal_items()
        {
            FeedProgramWith(new Item { Name = "Conjured Mana Cake", SellIn = 0, Quality = 50 });

            program.UpdateQuality();

            AssertOnItem(-1, 46);
        }

        private void AssertOnItem(int expectedSellIn, int expectedQuality)
        {
            Assert.Equal(expectedSellIn, program.Items[0].SellIn);
            Assert.Equal(expectedQuality, program.Items[0].Quality);
        }
    }
}