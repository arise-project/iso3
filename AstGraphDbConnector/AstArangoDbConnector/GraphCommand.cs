using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArangoDB.Client.Data;
using ArangoDB.Client.Examples.Models;

namespace ArangoDB.Client.Examples.Graphs
{
    public class GraphCommand
    {
        ArangoDatabase db = new ArangoDatabase(new DatabaseSharedSetting
        {
            Url = "http://localhost:8529",
            Database = "AstGraphDBConnector",
            Credential = new System.Net.NetworkCredential("root", "12345")
        });

        IArangoGraph Graph()
        {
            return db.Graph("SocialGraph");
        }

        GraphIdentifierResult CreateNewGraph()
        {
            var graph = Graph();

            return graph.Create(new List<EdgeDefinitionTypedData>
            {
                new EdgeDefinitionTypedData
                {
                    Collection = typeof(Follow),
                    From = new List<Type> { typeof(Person) },
                    To = new List<Type> { typeof(Person) }
                }
            });
        }

        // edge examples

        public void RemoveEdgeIfMatchFailed()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var v1 = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            var v2 = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            var follow = new Follow
            {
                Followee = v1.Id,
                Follower = v2.Id,
                CreatedDate = DateTime.Now
            };

            var inserted = graph.InsertEdge<Follow>(follow);
        }

        public void RemoveEdge()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var v1 = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            var v2 = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            var follow = new Follow
            {
                Followee = v1.Id,
                Follower = v2.Id,
                CreatedDate = DateTime.Now
            };

            var inserted = graph.InsertEdge<Follow>(follow);

            graph.RemoveEdge<Follow>(follow, ifMatchRev: inserted.Rev);

            var removed = graph.GetEdge<Follow>(inserted.Key);
        }

        public void RemoveEdgeById()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var v1 = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            var v2 = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            var follow = new Follow
            {
                Followee = v1.Id,
                Follower = v2.Id,
                CreatedDate = DateTime.Now
            };

            var inserted = graph.InsertEdge<Follow>(follow);

            graph.RemoveEdgeById<Follow>(inserted.Key);

            var removed = graph.GetEdge<Follow>(inserted.Key);
        }

        public void ReplaceEdgeIfMatchFailed()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var v1 = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            var v2 = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            var follow = new Follow
            {
                Followee = v1.Id,
                Follower = v2.Id,
                CreatedDate = DateTime.Now
            };

            var inserted = graph.InsertEdge<Follow>(follow);
        }

        public void ReplaceEdge()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var v1 = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            var v2 = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            var follow = new Follow
            {
                Followee = v1.Id,
                Follower = v2.Id,
                CreatedDate = DateTime.Now
            };

            var inserted = graph.InsertEdge<Follow>(follow);

            follow.CreatedDate = new DateTime(1900, 1, 1);

            graph.ReplaceEdge<Follow>(follow, ifMatchRev: inserted.Rev);

            var replaced = graph.GetEdge<Follow>(inserted.Key);
        }

        public void ReplaceEdgeById()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var v1 = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            var v2 = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            var follow = new Follow
            {
                Followee = v1.Id,
                Follower = v2.Id,
                CreatedDate = DateTime.Now
            };

            var inserted = graph.InsertEdge<Follow>(follow);

            graph.ReplaceEdgeById<Follow>(inserted.Key, new
            {
                _from = v1.Id,
                _to = v2.Id,
                CreatedDate = new DateTime(1900, 1, 1)
            });

            var replaced = graph.GetEdge<Follow>(inserted.Key);
        }

        public void UpdateEdgeIfMatchFailed()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var v1 = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            var v2 = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            var follow = new Follow
            {
                Followee = v1.Id,
                Follower = v2.Id,
                CreatedDate = DateTime.Now
            };

            var inserted = graph.InsertEdge<Follow>(follow);

            follow.CreatedDate = new DateTime(1900, 1, 1);
        }

        public void UpdateEdge()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var v1 = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            var v2 = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            var follow = new Follow
            {
                Followee = v1.Id,
                Follower = v2.Id,
                CreatedDate = DateTime.Now
            };

            var inserted = graph.InsertEdge<Follow>(follow);

            follow.CreatedDate = new DateTime(1900, 1, 1);

            graph.UpdateEdge<Follow>(follow, ifMatchRev: inserted.Rev);

            var updated = graph.GetEdge<Follow>(inserted.Key);
        }

        public void UpdateEdgeById()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var v1 = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            var v2 = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            var inserted = graph.InsertEdge<Follow>(new Follow
            {
                Followee = v1.Id,
                Follower = v2.Id,
                CreatedDate = DateTime.Now
            });

            graph.UpdateEdgeById<Follow>(inserted.Key, new { CreatedDate = new DateTime(1900, 1, 1) });

            var updated = graph.GetEdge<Follow>(inserted.Key);
        }

        public void InsertEdge()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var v1 = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            var v2 = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            var inserted = graph.InsertEdge<Follow>(new Follow
            {
                Followee = v1.Id,
                Follower = v2.Id
            });
        }

        public void GetEdge()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var v1 = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            var v2 = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            var inserted = graph.InsertEdge<Follow>(new Follow
            {
                Followee = v1.Id,
                Follower = v2.Id
            });

            var result = graph.GetEdge<Follow>(inserted.Key);
        }

        public void GetEdgeNotFound()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var result = graph.GetEdge<Follow>("none");
        }

        public void GetEdgeIfMatchFailed()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var v1 = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            var v2 = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            var inserted = graph.InsertEdge<Follow>(new Follow
            {
                Followee = v1.Id,
                Follower = v2.Id
            });

            var edgeInfo = db.FindDocumentInfo(inserted.Id);
        }

        // vertex examples

        public void RemoveVertexIfMatchFailed()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var person = new Person
            {
                Age = 21,
                Name = "raoof hojat"
            };

            var inserted = graph.InsertVertex<Person>(person);
        }

        public void RemoveVertex()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var person = new Person
            {
                Age = 21,
                Name = "raoof hojat"
            };

            var inserted = graph.InsertVertex<Person>(person);

            graph.RemoveVertex<Person>(person, ifMatchRev: inserted.Rev);

            var removed = graph.GetVertex<Person>(inserted.Key);
        }

        public void RemoveVertexById()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var inserted = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            graph.RemoveVertexById<Person>(inserted.Key);

            var removed = graph.GetVertex<Person>(inserted.Key);
        }

        public void ReplaceVertexIfMatchFailed()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var person = new Person
            {
                Age = 21,
                Name = "raoof hojat"
            };

            var inserted = graph.InsertVertex<Person>(person);

            person.Age = 33;
        }

        public void ReplaceVertex()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var person = new Person
            {
                Age = 21,
                Name = "raoof hojat"
            };

            var inserted = graph.InsertVertex<Person>(person);

            person.Age = 33;

            graph.ReplaceVertex<Person>(person, ifMatchRev: inserted.Rev);

            var replaced = graph.GetVertex<Person>(inserted.Key);
        }

        public void ReplaceVertexById()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var inserted = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            graph.ReplaceVertexById<Person>(inserted.Key, new { Age = 22 });

            var replaced = graph.GetVertex<Person>(inserted.Key);
        }

        public void UpdateVertexIfMatchFailed()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var person = new Person
            {
                Age = 21,
                Name = "raoof hojat"
            };

            var inserted = graph.InsertVertex<Person>(person);

            person.Age = 33;
        }

        public void UpdateVertex()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var person = new Person
            {
                Age = 21,
                Name = "raoof hojat"
            };

            var inserted = graph.InsertVertex<Person>(person);

            person.Age = 33;

            graph.UpdateVertex<Person>(person, ifMatchRev: inserted.Rev);

            var updated = graph.GetVertex<Person>(inserted.Key);
        }

        public void UpdateVertexById()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var inserted = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            graph.UpdateVertexById<Person>(inserted.Key, new { Age = 22 });

            var updated = graph.GetVertex<Person>(inserted.Key);
        }

        public void InsertVertex()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var result = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });
        }

        public void GetVertex()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var inserted = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            var result = graph.GetVertex<Person>(inserted.Key);
        }

        public void GetVertexNotFound()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var result = graph.GetVertex<Person>("none");
        }

        public void GetVertexIfMatchFailed()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var inserted = graph.InsertVertex<Person>(new Person
            {
                Age = 21,
                Name = "raoof hojat"
            });

            var vertexInfo = db.FindDocumentInfo(inserted.Id);
        }

        // management examples

        public void ListEdgeDefinitions()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var list = graph.ListEdgeDefinitions();
        }

        public void ExtendEdgeDefinitions()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var result = graph.Edge("Relation").ExtendDefinitions(
                new string[] { db.SharedSetting.Collection.ResolveCollectionName<Host>() },
                new string[] { db.SharedSetting.Collection.ResolveCollectionName<Host>() });
        }
        public void EditEdgeDefinition()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var result = graph.EditEdgeDefinition<Follow, Follow>(new List<Type> { typeof(Host) }, new List<Type> { typeof(Host) });
        }

        public void DeleteEdgeDefinition()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var result = graph.DeleteEdgeDefinition<Follow>();
        }

        public void ListVertexCollections()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            graph.AddVertexCollection<Host>();

            var result = graph.ListVertexCollections();
        }

        public void AddVertexCollection()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var result = graph.AddVertexCollection<Host>();
        }

        public void RemoveVertexCollection()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            graph.AddVertexCollection<Host>();

            var result = graph.RemoveVertexCollection<Host>();
        }

        public void Info()
        {
            var graph = Graph();

            var createdGraph = CreateNewGraph();

            var info = graph.Info();
        }

        public void ListGraphs()
        {
            var graph = Graph();

            CreateNewGraph();

            var allGraphs = db.ListGraphs();
        }

        public void Create()
        {
            var graph = Graph();

            var result = CreateNewGraph();
        }

        public void Drop()
        {
            var graph = Graph();

            CreateNewGraph();

            var dropped = graph.Drop();
        }
    }
}