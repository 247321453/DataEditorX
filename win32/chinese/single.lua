--created by DataEditorX
Debug.SetAIName("AI Name")
Debug.ReloadFieldBegin(DUEL_ATTACK_FIRST_TURN+DUEL_SIMPLE_AI)
Debug.SetPlayerInfo(0,8000,0,0)		--player
Debug.SetPlayerInfo(1,15000,0,0)	--AI
--Debug.AddCard(int code,int owner,int playerid,int location,int sequence,int position)
--Debug.AddCard(int code,int owner,int playerid,int location,int sequence,int position,bool revive_limit)
--Debug.PreAddCounter(Card card,int counter,int ccount)
--Debug.PreEquip(Card equip_card, Card target)
--Debug.PreSetTarget(Card card, Card target)
--end
Debug.ReloadFieldEnd()
Debug.ShowHint("Message")
aux.BeginPuzzle()
