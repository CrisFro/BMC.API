using System.ComponentModel.DataAnnotations;

namespace BMC.API.Models
{
    public enum BMCBlock
    {
        [Display(Name = "Parceiros Chave")]
        KeyPartners,

        [Display(Name = "Atividades Chave")]
        KeyActivities,

        [Display(Name = "Recursos Chave")]
        KeyResources,

        [Display(Name = "Propostas de Valor")]
        ValuePropositions,

        [Display(Name = "Relacionamento com Clientes")]
        CustomerRelationships,

        [Display(Name = "Canais")]
        Channels,

        [Display(Name = "Segmentos de Clientes")]
        CustomerSegments,

        [Display(Name = "Estrutura de Custos")]
        CostStructure,

        [Display(Name = "Fontes de Receita")]
        RevenueStreams
    }
   
}
