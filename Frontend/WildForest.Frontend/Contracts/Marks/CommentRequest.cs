using System;

namespace WildForest.Frontend.Contracts.Marks;

public sealed record CommentRequest(Guid WeatherId, Guid UserId, byte Rating, string Comment);