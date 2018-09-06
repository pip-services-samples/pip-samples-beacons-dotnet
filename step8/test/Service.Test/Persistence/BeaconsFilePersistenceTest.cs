﻿using System;
using System.Threading.Tasks;
using PipServices.Commons.Config;
using Xunit;

namespace Beacons.Persistence
{
    public class BeaconsFilePersistenceTest
    {
        private BeaconsFilePersistence _persistence;
        private BeaconsPersistenceFixture _fixture;

        public BeaconsFilePersistenceTest()
        {
            ConfigParams config = ConfigParams.FromTuples(
                "path", "beacons.json"
            );
            _persistence = new BeaconsFilePersistence();
            _persistence.Configure(config);
            _persistence.OpenAsync(null).Wait();
            _persistence.ClearAsync(null).Wait();

            _fixture = new BeaconsPersistenceFixture(_persistence);
        }

        [Fact]
        public async Task TestCrudOperationsAsync()
        {
            await _fixture.TestCrudOperationsAsync();
        }

        [Fact]
        public async Task TestGetWithFiltersAsync()
        {
            await _fixture.TestGetWithFiltersAsync();
        }
    }
}
