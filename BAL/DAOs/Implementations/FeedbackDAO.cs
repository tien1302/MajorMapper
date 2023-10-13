using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Feedbacks;
using DAL.Models;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BAL.DAOs.Implementations
{
    public class FeedbackDAO : IFeedbackDAO
    {
        private FeedbackRepository _feedbackRepository;
        private BookingRepository _bookingRepository;
        private IMapper _mapper;

        public FeedbackDAO(IFeedbackRepository feedbackRepository, IBookingRepository bookingRepository, IMapper mapper)
        {
            _feedbackRepository = (FeedbackRepository)feedbackRepository;
            _bookingRepository = (BookingRepository)bookingRepository;
            _mapper = mapper;
        }

        public void Create(CreateFeedback create)
        {
            try
            {
                var checkBookingId = _bookingRepository.GetByID(create.BookingId);
                if (checkBookingId == null)
                {
                    throw new Exception("Booking Id does not exist in the system.");
                }

                Feedback feedback = new Feedback()
                {
                    BookingId = create.BookingId,
                    Comment = create.Comment,
                    Star = create.Star,
                    CreateDateTime = DateTime.Now,
                };
                _feedbackRepository.Insert(feedback);
                _feedbackRepository.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int key)
        {
            try
            {
                Feedback existedFeedback = _feedbackRepository.GetByID(key);
                if (existedFeedback == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }
                _feedbackRepository.Delete(key);
                _feedbackRepository.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GetFeedback Get(int key)
        {
            try
            {
                List<GetFeedback> listFeedback = _mapper.Map<List<GetFeedback>>(_feedbackRepository.Get().ToList());
                Feedback feedback = _feedbackRepository.GetByID(key);

                if (feedback == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                GetFeedback result = listFeedback.FirstOrDefault(p => p.Id == feedback.Id);
                return _mapper.Map<GetFeedback>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GetFeedback> GetAll()
        {
            try
            {
                List<GetFeedback> listFeedback = _mapper.Map<List<GetFeedback>>(_feedbackRepository.Get().ToList());
                return listFeedback;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(int key, UpdateFeedback update)
        {
            try
            {
                var checkBookingId = _bookingRepository.GetByID(update.BookingId);
                if (checkBookingId == null)
                {
                    throw new Exception("Booking Id does not exist in the system.");
                }

                Feedback existedFeedback = _feedbackRepository.GetByID(key);
                if (existedFeedback == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                existedFeedback.BookingId = update.BookingId;
                existedFeedback.Comment  = update.Comment;
                existedFeedback.Star = update.Star;
                _feedbackRepository.Update(existedFeedback);
                _feedbackRepository.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
