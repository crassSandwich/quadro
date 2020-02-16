using System;
using System.Linq;

public class RandomOnTheBeatStrategy : IRhythmGeneratorStrategy
{
	public Random Random { get; set; }

	int beatsPerMeasure;

	bool[] currentMeasurePattern;

	public RandomOnTheBeatStrategy (int beatsPerMeasure)
	{
		this.beatsPerMeasure = beatsPerMeasure;
	}

	public PositionChunk GetPositionsForNextBeat (int positionInMeasure)
	{
		PositionChunk beat = new PositionChunk(positionInMeasure);

		if (currentMeasurePattern == null)
		{
			currentMeasurePattern = randomBeatPattern();
		}

		if (currentMeasurePattern[positionInMeasure])
		{
			beat.AddValue(positionInMeasure);
		}

		if (positionInMeasure >= currentMeasurePattern.Length - 1)
		{
			currentMeasurePattern = null;
		}

		return beat;
	}

	public void ClearPositionState ()
	{
		currentMeasurePattern = null;
	}

	bool[] randomBeatPattern ()
	{
        int uniqueCardPermutations = (1 << beatsPerMeasure) - 1;
		int index = Random.Next(1, uniqueCardPermutations);

        string paddedBinary = Convert.ToString(index, 2).PadLeft(beatsPerMeasure, '0');
        return paddedBinary.Select(c => c == '1').ToArray();
    }
}
