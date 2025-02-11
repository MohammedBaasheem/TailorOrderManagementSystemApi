using AutoMapper;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using System.Drawing;
using System.Linq;
using Tailor_Order_Management_System.Exceptions;
using Tailor_Order_Management_System.Models.DbContext;
using Tailor_Order_Management_System.Models.DTOs.Incoming;
using Tailor_Order_Management_System.Models.DTOs.Outgoing;
using Tailor_Order_Management_System.Models.EntityClasses;
using Tailor_Order_Management_System.Services.Interfaces;
using Color = Tailor_Order_Management_System.Models.EntityClasses.Color;
using KeyNotFoundException = System.Collections.Generic.KeyNotFoundException;

namespace Tailor_Order_Management_System.Services.Classes
{
    public class FabricService : IFabricService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public FabricService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<FabricDTO> AddFabricAsync(AddFabricDTO addFabricDTO)
        {
            //check if the fabric in the database or not.
            var existingfabric = await _context.Fabrics.FirstOrDefaultAsync(f => f.Name == addFabricDTO.Name);
            if (existingfabric is not null)
            {
                throw new BadRequestException("The Fabric is Added Before");
            }
            else
            {
                var fabric=_mapper.Map< Fabric >(addFabricDTO);
                //var fabric = new Fabric
                //{
                //    Name = addFabricDTO.Name,
                //    quantity = addFabricDTO.quantity,
                //};
                await _context.Fabrics.AddAsync(fabric);
                await _context.SaveChangesAsync();
                // check if the color are not empty 
                if (addFabricDTO.ColorsNames is not null)
                {
                    foreach (var colorName in addFabricDTO.ColorsNames)
                    {
                        var existingcolor = await _context.Colors.FirstOrDefaultAsync(c => c.Name == colorName);
                        if (existingcolor is not null)
                        {
                            var fabricColor = new FabricColor
                            {
                                FabricId = fabric.Id,
                                ColorId = existingcolor.Id
                            };
                            await _context.FabricColors.AddAsync(fabricColor);
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            var color = new Color
                            {
                                Name = colorName,
                            };
                            await _context.Colors.AddAsync(color);
                            await _context.SaveChangesAsync();
                            var fabricColor = new FabricColor
                            {
                                FabricId = fabric.Id,
                                ColorId = color.Id
                            };
                            await _context.FabricColors.AddAsync(fabricColor);
                            await _context.SaveChangesAsync();
                        }
                    }
                    //var colorfabric = _context.FabricColors.Where(fc => fc.FabricId == fabric.Id).Select(fc => fc.Color.Name).ToList();
                    var fabricDTO=_mapper.Map<FabricDTO>(fabric);
                   // fabricDTO.FabricColorsNmaes = colorfabric;
                    fabricDTO.Message = "Fabric added successfully.";
                    return fabricDTO;
                    //    new FabricDTO
                    //{
                    //    Message = "Fabric added successfully.",
                    //    Name = fabric.Name,
                    //    quantity = fabric.quantity,
                    //    FabricColorsNmaes = colorfabric
                    //};
                }
                else
                {
                    throw new KeyNotFoundException("Thy FabricColors Are Not Found");
                }
            }
        }
        public async Task<FabricDTO> UpdataFabricAsync(int FabricId, AddFabricDTO addFabricDTO)
        {
            var existingFabric = await _context.Fabrics.FindAsync(FabricId);
            if (existingFabric is not null)
            {

                existingFabric.Name = addFabricDTO.Name;
                existingFabric.quantity = addFabricDTO.quantity;

                await _context.SaveChangesAsync();
                // check if the color are not empty 
                if (addFabricDTO.ColorsNames is not null)
                {
                    foreach (var colorName in addFabricDTO.ColorsNames)
                    {
                        var existingcolor = await _context.Colors.FirstOrDefaultAsync(c => c.Name == colorName);
                        if (existingcolor is not null)
                        {
                            var fabricColor = new FabricColor
                            {
                                FabricId = existingFabric.Id,
                                ColorId = existingcolor.Id
                            };

                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            var color = new Color
                            {
                                Name = colorName,
                            };
                            await _context.Colors.AddAsync(color);
                            await _context.SaveChangesAsync();
                            var fabricColor = new FabricColor
                            {
                                FabricId = existingFabric.Id,
                                ColorId = color.Id
                            };
                            await _context.FabricColors.AddAsync(fabricColor);
                            await _context.SaveChangesAsync();
                        }
                    }
                    var fabricDTO = _mapper.Map<FabricDTO>(existingFabric);
                    fabricDTO.Message = "Fabric added successfully.";
                    return fabricDTO;
                }
                else
                {
                    throw new KeyNotFoundException("Thy FabricColors Are Not Found");
                    //return new FabricDTO { Message = "the fabriccolocrs is not found !" };
                }
            }
            else
            {
                throw new NotFoundException("Thy FabricColors Are Not Found");
            }
        }
        public async Task<List<FabricDTO>> GetAllFabricsAsync()
        {
            var fabrics =await _context.Fabrics.Include(fc=>fc.FabricColors).ThenInclude(c=>c.Color).ToListAsync();
            if(fabrics is not null)
            {
                var fabricDTO = _mapper.Map<List<FabricDTO>>(fabrics);
                return fabricDTO;
            }
            else
            {
                throw new NotFoundException("There Are  No Fabrices  Found");
            }
        }
        public async Task<FabricDTO> GetFabricAsync(int FabricId)
        {
            var fabric =await _context.Fabrics.Include(fc => fc.FabricColors).ThenInclude(c => c.Color).FirstOrDefaultAsync(f => f.Id == FabricId);
            if(fabric is not null)
            {
                var fabricDTO = _mapper.Map<FabricDTO>(fabric);
                

                return fabricDTO;
            }
            else
            {
                throw new KeyNotFoundException("The Fabric Not Found");
            }
            
            
        }
        public async Task<string> DeleteFabricAsync(int FabricId)
        {
            var fabric = await _context.Fabrics.FirstOrDefaultAsync(f => f.Id == FabricId);
            if (fabric is not null)
            {
               _context.Fabrics.Remove(fabric);
                await _context.SaveChangesAsync();

                return "The Fabric Deleted successfully.";
            }
            else
            {
                throw new KeyNotFoundException("The Fabric Not Found");
            }
        }



    }
}
