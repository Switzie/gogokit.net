﻿using System;
using System.Runtime.Serialization;
using HalKit.Json;
using HalKit.Models.Response;

namespace GogoKit.Models.Response
{
    /// <summary>
    /// An event on the viagogo platform.
    /// </summary>
    public class Event : EmbeddedEvent
    {
        [DataMember(Name = "end_date")]
        public DateTimeOffset? EndDate { get; set; }

        [DataMember(Name = "date_confirmed")]
        public bool DateConfirmed { get; set; }

        [DataMember(Name = "min_ticket_price")]
        public Money MinTicketPrice { get; set; }

        [DataMember(Name = "notes_html")]
        public string Notes { get; set; }

        [DataMember(Name = "restrictions_html")]
        public string Restrictions { get; set; }

        [Embedded("venue")]
        public EmbeddedVenue Venue { get; set; }

        [Embedded("category")]
        public EmbeddedCategory Category { get; set; }

        /// <summary>
        /// You can GET the href of this link to retrieve the <see cref="Category"/>
        /// resource that contains an <see cref="Event"/>.
        /// </summary>
        /// <remarks>See http://developer.viagogo.net/#eventcategory.</remarks>
        [Rel("event:category")]
        public Link CategoryLink { get; set; }

        /// <summary>
        /// You can GET the href of this link to retrieve the <see cref="Listing"/>
        /// resources in an event.
        /// </summary>
        /// <remarks>See http://developer.viagogo.net/#eventlistings.</remarks>
        [Rel("event:listings")]
        public Link ListingsLink { get; set; }

        /// <summary>
        /// You can GET the href of this link to retrieve the local viagogo website webpage
        /// for an event.
        /// </summary>
        /// <remarks>See http://developer.viagogo.net/#eventlocalwebpage.</remarks>
        [Rel("event:localwebpage")]
        public Link LocalWebPageLink { get; set; }

        /// <summary>
        /// You can GET the href of this link to retrieve the viagogo website
        /// webpage for an event.
        /// </summary>
        /// <remarks>See http://developer.viagogo.net/#eventwebpage.</remarks>
        [Rel("event:webpage")]
        public Link WebPageLink { get; set; }
    }
}