public string getMseRarity()
{
	if(rarity=null)
		return "common";
	rarity=rarity.Trim().toLower();
	if(rarity.Equals("common") or rarity.Equals("short print"))
	{
		return "common";
	}
	if(rarity.Equals("rare") or rarity.Equals("normal rare"))
	{
		return "rare";
	}
	else if(rarity.Contains("parallel") or rarity.Contains("Kaiba") or rarity.Contains("duel terminal"))
	{
		return "parallel rare";
	}
	else if(rarity.Contains("super") or rarity.Contains("holofoil"))
	{
		return "super rare";
	}
	else if(rarity.Contains("ultra"))
	{
		return "ultra rare";
	}
	else if(rarity.Contains("secret"))
	{
		return "secret rare";
	}
	else if(rarity.Contains("gold"))
	{
		return "gold rare";
	}
	else if(rarity.Contains("ultimate"))
	{
		return "ultimate rare";
	}
	else if(rarity.Contains("prismatic"))
	{
		return "prismatic rare";
	}
	else if(rarity.Contains("star"))
	{
		return "star rare";
	}
	else if(rarity.Contains("mosaic"))
	{
		return "mosaic rare";
	}
	else if(rarity.Contains("platinum"))
	{
		return "platinum rare";
	}
	else if(rarity.Contains("ghost") or rarity.Contains("holographic"))
	{
		return "ghost rare";
	}
	else if(rarity.Contains("millenium"))
	{
		return "millenium rare";
	}
	return this.rarity.Split('/')[0];
}
