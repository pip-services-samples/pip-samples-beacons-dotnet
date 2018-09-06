﻿using PipServices.Commons.Config;
using PipServices.Commons.Refer;
using System.Threading.Tasks;
using Xunit;
using Beacons.Persistence;
using Beacons.Logic;
using Beacons.Services.Version1;
using System;

namespace Beacons.Clients.Version1
{
    public class BeaconsDirectClientV1Test
    {
        private BeaconsMemoryPersistence _persistence;
        private BeaconsController _controller;
        private BeaconsDirectClientV1 _client;
        private BeaconsClientV1Fixture _fixture;

        public BeaconsDirectClientV1Test()
        {
            _persistence = new BeaconsMemoryPersistence();
            _controller = new BeaconsController();
            _client = new BeaconsDirectClientV1();

            IReferences references = References.FromTuples(
                new Descriptor("beacons", "persistence", "memory", "default", "1.0"), _persistence,
                new Descriptor("beacons", "controller", "default", "default", "1.0"), _controller,
                new Descriptor("beacons", "client", "direct", "default", "1.0"), _client
            );

            _controller.SetReferences(references);

            _client.SetReferences(references);

            _fixture = new BeaconsClientV1Fixture(_client);

            _client.OpenAsync(null).Wait();
        }

        [Fact]
        public async Task TestCrudOperationsAsync()
        {
            await _fixture.TestCrudOperationsAsync();
        }

        [Fact]
        public async Task TestCalculatePositionsAsync()
        {
            await _fixture.TestCalculatePositionsAsync();
        }

    }
}
