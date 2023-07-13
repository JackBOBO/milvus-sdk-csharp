﻿using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace IO.Milvus;

/// <summary>
/// Milvus compaction plans.
/// </summary>
public sealed class MilvusCompactionPlans
{
    /// <summary>
    /// Merge infos.
    /// </summary>
    public IList<MilvusCompactionPlan> MergeInfos { get; }

    /// <summary>
    /// State.
    /// </summary>
    public MilvusCompactionState State { get; }

    internal static MilvusCompactionPlans From(Grpc.GetCompactionPlansResponse response)
    {
        return new MilvusCompactionPlans(response.MergeInfos.Select(static x => new MilvusCompactionPlan()
        {
            Sources = x.Sources,
            Target = x.Target
        }), (MilvusCompactionState)response.State);
    }

    #region Private =========================================================================================================
    private MilvusCompactionPlans(
        IEnumerable<MilvusCompactionPlan> collection,
        MilvusCompactionState state)
    {
        MergeInfos = collection.ToList();
        State = state;
    }
    #endregion
}

/// <summary>
/// Milvus compaction plan.
/// </summary>
public sealed class MilvusCompactionPlan
{
    /// <summary>
    /// Sources
    /// </summary>
    public required IList<long> Sources { get; set; }

    /// <summary>
    /// Target
    /// </summary>
    public long Target { get; set; }
}
