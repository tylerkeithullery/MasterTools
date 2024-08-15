// <auto-generated />
#pragma warning disable CS0105
using MackySoft.MasterTools.Example;
using MasterMemory.Validation;
using MasterMemory;
using MessagePack;
using System.Collections.Generic;
using System;
using MackySoft.MasterTools.Example.MasterData.Tables;

namespace MackySoft.MasterTools.Example.MasterData
{
   public sealed class ImmutableBuilder : ImmutableBuilderBase
   {
        MemoryDatabase memory;

        public ImmutableBuilder(MemoryDatabase memory)
        {
            this.memory = memory;
        }

        public MemoryDatabase Build()
        {
            return memory;
        }

        public void ReplaceAll(System.Collections.Generic.IList<ItemMasterData> data)
        {
            var newData = CloneAndSortBy(data, x => x.Id, System.Collections.Generic.Comparer<int>.Default);
            var table = new ItemMasterDataTable(newData);
            memory = new MemoryDatabase(
                table,
                memory.QuestMasterDataTable
            
            );
        }

        public void RemoveItemMasterData(int[] keys)
        {
            var data = RemoveCore(memory.ItemMasterDataTable.GetRawDataUnsafe(), keys, x => x.Id, System.Collections.Generic.Comparer<int>.Default);
            var newData = CloneAndSortBy(data, x => x.Id, System.Collections.Generic.Comparer<int>.Default);
            var table = new ItemMasterDataTable(newData);
            memory = new MemoryDatabase(
                table,
                memory.QuestMasterDataTable
            
            );
        }

        public void Diff(ItemMasterData[] addOrReplaceData)
        {
            var data = DiffCore(memory.ItemMasterDataTable.GetRawDataUnsafe(), addOrReplaceData, x => x.Id, System.Collections.Generic.Comparer<int>.Default);
            var newData = CloneAndSortBy(data, x => x.Id, System.Collections.Generic.Comparer<int>.Default);
            var table = new ItemMasterDataTable(newData);
            memory = new MemoryDatabase(
                table,
                memory.QuestMasterDataTable
            
            );
        }

        public void ReplaceAll(System.Collections.Generic.IList<QuestMasterData> data)
        {
            var newData = CloneAndSortBy(data, x => x.Id, System.Collections.Generic.Comparer<int>.Default);
            var table = new QuestMasterDataTable(newData);
            memory = new MemoryDatabase(
                memory.ItemMasterDataTable,
                table
            
            );
        }

        public void RemoveQuestMasterData(int[] keys)
        {
            var data = RemoveCore(memory.QuestMasterDataTable.GetRawDataUnsafe(), keys, x => x.Id, System.Collections.Generic.Comparer<int>.Default);
            var newData = CloneAndSortBy(data, x => x.Id, System.Collections.Generic.Comparer<int>.Default);
            var table = new QuestMasterDataTable(newData);
            memory = new MemoryDatabase(
                memory.ItemMasterDataTable,
                table
            
            );
        }

        public void Diff(QuestMasterData[] addOrReplaceData)
        {
            var data = DiffCore(memory.QuestMasterDataTable.GetRawDataUnsafe(), addOrReplaceData, x => x.Id, System.Collections.Generic.Comparer<int>.Default);
            var newData = CloneAndSortBy(data, x => x.Id, System.Collections.Generic.Comparer<int>.Default);
            var table = new QuestMasterDataTable(newData);
            memory = new MemoryDatabase(
                memory.ItemMasterDataTable,
                table
            
            );
        }

    }
}