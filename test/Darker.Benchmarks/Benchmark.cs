﻿using System;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Darker.Builder;

namespace Darker.Benchmarks
{
    public class Benchmark
    {
        private readonly IQueryProcessor _queryProcessor;

        public Benchmark()
        {
            var handlerRegistry = new QueryHandlerRegistry();
            handlerRegistry.Register<BasicSyncQuery, bool, BasicSyncQueryHandler>();
            handlerRegistry.Register<BasicAsyncQuery, bool, BasicAsyncQueryHandler>();

            _queryProcessor = QueryProcessorBuilder.With()
                .Handlers(handlerRegistry, Activator.CreateInstance, Activator.CreateInstance)
                .InMemoryQueryContextFactory()
                .Build();
        }

        [Benchmark]
        public void BasicSyncQuery()
        {
            _queryProcessor.Execute(new BasicSyncQuery());
        }

        [Benchmark]
        public async Task BasicAsyncQuery()
        {
            await _queryProcessor.ExecuteAsync(new BasicAsyncQuery());
        }
    }
}