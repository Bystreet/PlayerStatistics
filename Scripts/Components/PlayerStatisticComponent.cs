﻿// =======================================================================================
// Wovencore
// by Weaver (Fhiz)
// MIT licensed
// =======================================================================================

using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Wovencode;

namespace Wovencode {
	
	// ===================================================================================
	// PlayerStatisticComponent
	// ===================================================================================
	[DisallowMultipleComponent]
	[System.Serializable]
	public partial class PlayerStatisticComponent : SyncableComponent
	{
		
		protected SyncListStatisticSyncStruct syncData = new SyncListStatisticSyncStruct();
		
		// -------------------------------------------------------------------------------
		// AddEntry
		// -------------------------------------------------------------------------------
		[Server]
		public void AddEntry(string _name, string _category, long _value)
		{
			StatisticSyncStruct syncStruct = new StatisticSyncStruct(_name, _category, _value);
			syncData.Add(syncStruct);
		}
		
		// -------------------------------------------------------------------------------
		// GetEntries
		// -------------------------------------------------------------------------------
		public List<StatisticSyncStruct> GetEntries(bool validOnly=true, SortOrder _sortOrder=SortOrder.None, string _category="")
		{
		
			List<StatisticSyncStruct> entryList = new List<StatisticSyncStruct>();
			
			foreach (StatisticSyncStruct entry in syncData)
			{
				if (entry.Valid)
				{
					
					if (string.IsNullOrWhiteSpace(_category) ||
						entry.category == _category)
						entryList.Add(entry);
				
				}
				else if (!validOnly && !entry.Valid)
					entryList.Add(entry);
			
			}
			
			if (validOnly && _sortOrder == SortOrder.Name)
				entryList.OrderBy(x => x.name).ToList();
			
			return entryList;
		
		}
		
		// -------------------------------------------------------------------------------
		// UpdateServer
		// -------------------------------------------------------------------------------
		[Server]
		protected override void UpdateServer()
		{
			/* not used yet */
		}
		
		// -------------------------------------------------------------------------------
		// UpdateClient
		// -------------------------------------------------------------------------------
		[Client]
		protected override void UpdateClient()
		{
			/* not used yet */
		}
		
		// -------------------------------------------------------------------------------
	
	}

}

// =======================================================================================