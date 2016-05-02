﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GogoKit.Models.Request;
using GogoKit.Models.Response;
using GogoKit.Services;
using HalKit;
using HalKit.Http;
using HalKit.Models.Response;

namespace GogoKit.Clients
{
    public class SellerListingsClient : ISellerListingsClient
    {
        private readonly IHalClient _halClient;
        private readonly ILinkFactory _linkFactory;

        public SellerListingsClient(IHalClient halClient, ILinkFactory linkFactory)
        {
            Requires.ArgumentNotNull(halClient, nameof(halClient));
            Requires.ArgumentNotNull(linkFactory, nameof(linkFactory));

            _halClient = halClient;
            _linkFactory = linkFactory;
        }

        public Task<SellerListing> GetAsync(int sellerListingId)
        {
            return GetAsync(sellerListingId, new SellerListingRequest());
        }

        public async Task<SellerListing> GetAsync(int sellerListingId, SellerListingRequest request)
        {
            var listingLink = await _linkFactory.CreateLinkAsync($"sellerlistings/{sellerListingId}").ConfigureAwait(_halClient);
            return await _halClient.GetAsync<SellerListing>(listingLink, request).ConfigureAwait(_halClient);
        }

        public Task<PagedResource<SellerListing>> GetAsync()
        {
            return GetAsync(new SellerListingRequest());
        }

        public async Task<PagedResource<SellerListing>> GetAsync(SellerListingRequest request)
        {
            var sellerListingsLink = await _linkFactory.CreateLinkAsync("sellerlistings").ConfigureAwait(_halClient);
            return await _halClient.GetAsync<PagedResource<SellerListing>>(sellerListingsLink, request).ConfigureAwait(_halClient);
        }

        public Task<IReadOnlyList<SellerListing>> GetAllAsync()
        {
            return GetAllAsync(new SellerListingRequest());
        }

        public Task<IReadOnlyList<SellerListing>> GetAllAsync(SellerListingRequest request)
        {
            return GetAllAsync(request, CancellationToken.None);
        }

        public async Task<IReadOnlyList<SellerListing>> GetAllAsync(SellerListingRequest request, CancellationToken cancellationToken)
        {
            var sellerListingsLink = await _linkFactory.CreateLinkAsync("sellerlistings").ConfigureAwait(_halClient);
            return await _halClient.GetAllPagesAsync<SellerListing>(
                            sellerListingsLink,
                            request,
                            cancellationToken).ConfigureAwait(_halClient);
        }

        public async Task<ChangedResources<SellerListing>> GetAllChangesAsync()
        {
            var sellerListingsLink = await _linkFactory.CreateLinkAsync("sellerlistings").ConfigureAwait(_halClient);
            return await GetAllChangesAsync(sellerListingsLink).ConfigureAwait(_halClient);
        }

        public Task<ChangedResources<SellerListing>> GetAllChangesAsync(Link nextLink)
        {
            return GetAllChangesAsync(nextLink, new SellerListingRequest());
        }

        public Task<ChangedResources<SellerListing>> GetAllChangesAsync(
            Link nextLink,
            SellerListingRequest request)
        {
            return GetAllChangesAsync(nextLink, request, CancellationToken.None);
        }

        public Task<ChangedResources<SellerListing>> GetAllChangesAsync(
            Link nextLink,
            SellerListingRequest request,
            CancellationToken cancellationToken)
        {
            return _halClient.GetChangedResourcesAsync<SellerListing>(nextLink, request, cancellationToken);
        }

        public Task<ListingConstraints> GetConstraintsAsync(int sellerListingId)
        {
            return GetConstraintsAsync(sellerListingId, new ListingConstraintsRequest());
        }

        public async Task<ListingConstraints> GetConstraintsAsync(int sellerListingId, ListingConstraintsRequest request)
        {
            var constraintsLink = await _linkFactory.CreateLinkAsync($"sellerlistings/{sellerListingId}/constraints").ConfigureAwait(_halClient);
            return await _halClient.GetAsync<ListingConstraints>(constraintsLink, request).ConfigureAwait(_halClient);
        }

        public Task<ListingConstraints> GetConstraintsForEventAsync(int eventId)
        {
            return GetConstraintsAsync(eventId, new ListingConstraintsRequest());
        }

        public async Task<ListingConstraints> GetConstraintsForEventAsync(int eventId, ListingConstraintsRequest request)
        {
            var constraintsLink = await _linkFactory.CreateLinkAsync($"events/{eventId}/listingconstraints").ConfigureAwait(_halClient);
            return await _halClient.GetAsync<ListingConstraints>(constraintsLink, request).ConfigureAwait(_halClient);
        }

        public async Task<SellerListingPreview> CreateSellerListingPreviewAsync(int eventId, NewSellerListing listing)
        {
            var previewLink = await _linkFactory.CreateLinkAsync($"events/{eventId}/sellerlistingpreview").ConfigureAwait(_halClient);
            return await _halClient.PostAsync<SellerListingPreview>(previewLink, listing).ConfigureAwait(_halClient);
        }

        public async Task<SellerListingPreview> CreateSellerListingUpdatePreviewAsync(int sellerListingId, SellerListingUpdate listingUpdate)
        {
            var previewLink = await _linkFactory.CreateLinkAsync($"sellerlistings/{sellerListingId}/updatepreview").ConfigureAwait(_halClient);
            return await _halClient.PostAsync<SellerListingPreview>(previewLink, listingUpdate).ConfigureAwait(_halClient);
        }

        public Task<SellerListing> CreateAsync(NewRequestedEventSellerListing listing)
        {
            return CreateAsync(listing, new SellerListingRequest());
        }

        public Task<SellerListing> CreateAsync(NewRequestedEventSellerListing listing, SellerListingRequest request)
        {
            return CreateAsync(listing, new SellerListingRequest(), CancellationToken.None);
        }

        public async Task<SellerListing> CreateAsync(
            NewRequestedEventSellerListing listing,
            SellerListingRequest request,
            CancellationToken cancellationToken)
        {
            var createListingLink = await _linkFactory.CreateLinkAsync("sellerListings").ConfigureAwait(_halClient);
            return await _halClient.PostAsync<SellerListing>(createListingLink, listing, request, cancellationToken).ConfigureAwait(_halClient);
        }

        public Task<SellerListing> CreateAsync(int eventId, NewSellerListing listing)
        {
            return CreateAsync(eventId, listing, new SellerListingRequest());
        }

        public Task<SellerListing> CreateAsync(int eventId, NewSellerListing listing, SellerListingRequest request)
        {
            return CreateAsync(eventId, listing, request, CancellationToken.None);
        }

        public async Task<SellerListing> CreateAsync(
            int eventId,
            NewSellerListing listing,
            SellerListingRequest request,
            CancellationToken cancellationToken)
        {
            var createListingLink = await _linkFactory.CreateLinkAsync($"events/{eventId}/sellerlistings").ConfigureAwait(_halClient);
            return await _halClient.PostAsync<SellerListing>(createListingLink, listing, request, cancellationToken).ConfigureAwait(_halClient);
        }

        public Task<SellerListing> UpdateAsync(int sellerListingId, SellerListingUpdate listingUpdate)
        {
            return UpdateAsync(sellerListingId, listingUpdate, new SellerListingRequest());
        }

        public async Task<SellerListing> UpdateAsync(int sellerListingId, SellerListingUpdate listingUpdate, SellerListingRequest request)
        {
            var updateLink = await _linkFactory.CreateLinkAsync($"sellerlistings/{sellerListingId}").ConfigureAwait(_halClient);
            return await _halClient.PatchAsync<SellerListing>(updateLink, listingUpdate, request).ConfigureAwait(_halClient);
        }

        public async Task<IApiResponse> DeleteAsync(int sellerListingId)
        {
            var deleteLink = await _linkFactory.CreateLinkAsync($"sellerlistings/{sellerListingId}").ConfigureAwait(_halClient);
            return await _halClient.DeleteAsync(deleteLink);
        }
    }
}