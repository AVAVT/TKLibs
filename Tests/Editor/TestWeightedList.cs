using NUnit.Framework;
using TKLibs;
using Random = System.Random;

public class TestWeightedList
{
    [Test]
    public void TestWeightedList_SimpleFlow()
    {
        var weightedList = new WeightedList<string>();
        weightedList.AddOrReplace(TEST_STRING_1);

        var result = weightedList.RandomItem();
        
        Assert.AreEqual(TEST_STRING_1, result);
    }

    [Test]
    public void TestWeightedList_ProceduralRandom()
    {
        Random testRandom = new(TEST_SEED);
        var weightedList = new WeightedList<string>(testRandom);
        weightedList.AddOrReplace(TEST_STRING_2);
        weightedList.AddOrReplace(TEST_STRING_1);

        var result = weightedList.RandomItem();
        
        Assert.AreEqual(TEST_STRING_2, result);
    }

    [Test]
    public void TestWeightedList_RoughlyEqualChance()
    {
        Random testRandom = new(TEST_SEED);
        var weightedList = new WeightedList<string>(testRandom);
        weightedList.AddOrReplace(TEST_STRING_1);
        weightedList.AddOrReplace(TEST_STRING_2);
        weightedList.AddOrReplace(TEST_STRING_3);
        weightedList.AddOrReplace(TEST_STRING_4);
        
        var results = new[]{0,0,0,0};
        const int TEST_LOOP_COUNT = 100000; // weight 1 should appear ~ 25000 times

        for (var i = 0; i < TEST_LOOP_COUNT; i++)
        {
            switch (weightedList.RandomItem())
            {
                case TEST_STRING_1: 
                    results[0]++;
                    break;
                case TEST_STRING_2: 
                    results[1]++;
                    break;
                case TEST_STRING_3: 
                    results[2]++;
                    break;
                case TEST_STRING_4: 
                    results[3]++;
                    break;
            }
        }
        
        Assert.AreEqual(new[]{25037,25213,24771,24979}, results);
    }
    
    [Test]
    public void TestWeightedList_WeightBasedChance()
    {
        Random testRandom = new(TEST_SEED);
        var weightedList = new WeightedList<string>(testRandom);
        weightedList.AddOrReplace(new WeightedItem<string>(TEST_STRING_1, 1));
        weightedList.AddOrReplace(new WeightedItem<string>(TEST_STRING_2, 2));
        weightedList.AddOrReplace(new WeightedItem<string>(TEST_STRING_3, 3));
        
        var results = new[]{0,0,0};
        const int TEST_LOOP_COUNT = 120000; // weight 1 should appear ~ 20000 times

        for (var i = 0; i < TEST_LOOP_COUNT; i++)
        {
            switch (weightedList.RandomItem())
            {
                case TEST_STRING_1: 
                    results[0]++;
                    break;
                case TEST_STRING_2: 
                    results[1]++;
                    break;
                case TEST_STRING_3: 
                    results[2]++;
                    break;
            }
        }
        
        Assert.AreEqual(new[]{20122,40231,59647}, results);
    }

    [Test]
    public void TestWeightedList_CanAddDefaultWeight()
    {
        var weightedList = new WeightedList<string>();
        
        var weightedItem = weightedList.AddOrReplace(TEST_STRING_1);
        
        Assert.AreEqual(1, weightedItem.Weight);
    }
    
    [Test]
    public void TestWeightedList_CanAddDefinedWeight()
    {
        var weightedList = new WeightedList<string>();
        
        var weightedItem = weightedList.AddOrReplace( new WeightedItem<string>(TEST_STRING_1, 12));
        
        Assert.AreEqual(12, weightedItem.Weight);
    }
    
    [Test]
    public void TestWeightedList_CanReplace()
    {
        var weightedList = new WeightedList<string>();
        weightedList.AddOrReplace(TEST_STRING_1);
        var itemToReplace = new WeightedItem<string>(TEST_STRING_1, 6);

        weightedList.AddOrReplace(itemToReplace);
        
        Assert.AreEqual(itemToReplace, weightedList.GetWeightedItem(TEST_STRING_1));
    }

    [Test]
    public void TestWeightedList_CanRemove()
    {
        var weightedList = new WeightedList<string>();
        weightedList.AddOrReplace(TEST_STRING_1);
        
        weightedList.Remove(TEST_STRING_1);
        
        Assert.IsNull(weightedList.GetWeightedItem(TEST_STRING_1));
    }

    [Test]
    public void TestWeightedList_RemovingOnlyConsiderKey()
    {
        var weightedList = new WeightedList<string>();
        weightedList.AddOrReplace(TEST_STRING_1);
        
        weightedList.Remove( new WeightedItem<string>(TEST_STRING_1, 12));
        
        Assert.IsNull(weightedList.GetWeightedItem(TEST_STRING_1));
    }

    [Test]
    public void TestWeightedList_RemoveRandom()
    {
        var weightedList = new WeightedList<string>();
        weightedList.AddOrReplace(TEST_STRING_1);
        weightedList.AddOrReplace(TEST_STRING_2);
        weightedList.AddOrReplace(TEST_STRING_3);
        weightedList.AddOrReplace(TEST_STRING_4);

        var removedItem = weightedList.RemoveRandomItem();
        
        Assert.IsNull(weightedList.GetWeightedItem(removedItem));
    }
    
    const int TEST_SEED = 1337;

    const string TEST_STRING_1 = "TEST_STRING_1";
    const string TEST_STRING_2 = "TEST_STRING_2";
    const string TEST_STRING_3 = "TEST_STRING_3";
    const string TEST_STRING_4 = "TEST_STRING_4";
}
