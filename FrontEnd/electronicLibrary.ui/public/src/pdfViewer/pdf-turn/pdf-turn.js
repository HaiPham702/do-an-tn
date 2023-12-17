var bookFlip = {
  _width: [],
  _height: [],
  active: false,
  _spreadBk: NaN,
  _evSpread: null,
  _spread: NaN,
  toStart: false,
  _intoView: null,
  _visPages: null,
  _ready: false,
  _toDoublePage: false,
  init: function () {
    _toDoublePage = false
    $(document).on('rotationchanging', () => {
      this.rotate()
    })
    $(document).on('scalechanging', () => {
      this.resize()
    })
    $(document).on('pagechanging', () => {
      this.flip()
    })
    $(document).on('documentinit', () => {
      this.stop()
      this._ready = false
    })
    $(document).on('scrollmodechanged', () => {
      var scroll = PDFViewerApplication.pdfViewer.scrollMode
      if (scroll === 3) this.start()
      else this.stop()
      var button = PDFViewerApplication.appConfig.secondaryToolbar.bookFlipButton
      button.classList.toggle('toggled', scroll === 3)
    })
    $(document).on('switchspreadmode', (evt) => {
      if (evt.originalEvent.detail.mode === 0) {
        document.getElementById('spreadNone').style.display = 'none'
        document.getElementById('spreadEven').style.display = 'inline-block'
        PDFViewerApplication.pdfViewer.container.classList.remove('singlePage')
      } else {
        if (PDFViewerApplication.pdfViewer.currentPageNumber === 1) {
        }
        document.getElementById('spreadNone').style.display = 'inline-block'
        document.getElementById('spreadEven').style.display = 'none'
      }
      if (
        evt.originalEvent.detail.mode === 2 &&
        PDFViewerApplication.pdfViewer.currentPageNumber === 1
      ) {
        _toDoublePage = true
      } else {
        _toDoublePage = false
        switchSpreadMode(evt)
      }
    })
    $(document).on('pagesloaded', () => {
      this._ready = true
      if (this.toStart) {
        this.toStart = false
        PDFViewerApplication.pdfViewer.scrollMode = 3
      }
    })
    $(document).on('baseviewerinit', () => {
      PDFViewerApplicationOptions.set('scrollModeOnLoad', 3)
      this._intoView = PDFViewerApplication.pdfViewer.scrollPageIntoView
      this._visPages = PDFViewerApplication.pdfViewer._getVisiblePages
    })
  },
  start: function (event, pageObject, corner) {
    if (this.active || !this._ready) return
    this.active = true
    var viewer = PDFViewerApplication.pdfViewer
    $('.scrollModeButtons').removeClass('toggled')
    this._spreadBk = viewer.spreadMode
    var selected = $('.spreadModeButtons.toggled').attr('id')
    this._spread = this._spreadBk !== 2 ? 0 : 2
    viewer.spreadMode = 0
    viewer._spreadMode = -1
    $('.spreadModeButtons').removeClass('toggled')
    $('#' + selected).addClass('toggled')
    this._evSpread = PDFViewerApplication.eventBus._listeners.switchspreadmode
    PDFViewerApplication.eventBus._listeners.switchspreadmode = null
    viewer.scrollPageIntoView = (data) => {
      return this.link(data)
    }
    viewer._getVisiblePages = () => {
      return this.load()
    }
    var scale = viewer.currentScale
    var parent = this
    $('#viewer .page').each(function () {
      parent._width[$(this).attr('data-page-number')] = $(this).width() / scale
      parent._height[$(this).attr('data-page-number')] = $(this).height() / scale
    })
    $('#viewer').removeClass('pdfViewer').addClass('bookViewer').css({ opacity: 1 })
    $('#spreadOdd').prop('disabled', true)
    var pages = PDFViewerApplication.pagesCount
    var oTurn = $('#viewer').turn({
      elevation: 50,
      width: this._size(PDFViewerApplication.page, 'width') * this._spreadMult(),
      height: this._size(PDFViewerApplication.page, 'height'),
      page: PDFViewerApplication.page,
      when: {
        turned: function (event, page) {
          PDFViewerApplication.page = page
          if (page === 1 && isSinglePage()) {
            PDFViewerApplication.pdfViewer.container.classList.add('singlePage')
            PDFViewerApplication.pdfViewer.container.classList.remove('paper')
          }
          viewer.update()
        }
      },
      display: this._spreadType(),
      duration: 600,
      peel: false
    })
    $('#previous-paper').click(function (e) {
      e.preventDefault()
      oTurn.turn('previous')
    })
    $('#next-paper').click(function (e) {
      e.preventDefault()
      nextPage()
    })
    $('.pageDown').click(function (e) {
      e.preventDefault()
      nextPage()
    })
    $('.pageUp').click(function (e) {
      e.preventDefault()
      oTurn.turn('previous')
    })
    $('#pageNumber').change(function (e) {
      e.preventDefault()
      var turnNum = 1
      if (!e.target.value) {
        turnNum = 1
        e.target.value = turnNum
      } else {
        turnNum = parseInt(e.target.value)
        if (turnNum <= 0) {
          turnNum = 1
          e.target.value = turnNum
        } else if (turnNum > PDFViewerApplication.pagesCount) {
          turnNum = PDFViewerApplication.pagesCount
          e.target.value = turnNum
        }
      }
      var currnetPage = PDFViewerApplication.pdfViewer.currentPageNumber
      if (currnetPage === 1 && turnNum !== 1) {
        if (!isSinglePage()) {
          viewerContainer.classList.remove('singlePage')
          viewerContainer.classList.add('paper')
        }
        setTimeout(() => {
          oTurn.turn('page', turnNum)
        }, 350)
      } else {
        oTurn.turn('page', turnNum)
      }
    })
    $(document).on('keydown', function (event) {
      var keyCode = event.which
      switch (keyCode) {
        case 39:
          nextPage()
          break
        case 37:
          oTurn.turn('previous')
      }
    })
    const pdfView = document.getElementById('viewer')
    const viewerContainer = document.getElementById('viewerContainer')
    let startX = 0
    let startY = 0
    pdfView.addEventListener('mousedown', (event) => {
      startX = event.clientX
    })
    pdfView.addEventListener('mouseup', (event) => {
      const endX = event.clientX
      const deltaX = startX - endX
      if (deltaX > 50 && PDFViewerApplication.pdfCursorTools.active === 0) {
        nextPage()
      }
    })
    pdfView.addEventListener('mouseup', (event) => {
      const endX = event.clientX
      const deltaX = startX - endX
      if (deltaX < -50 && PDFViewerApplication.pdfCursorTools.active === 0) {
        oTurn.turn('previous')
      }
    })
    let endToucheX = 0
    let initialDistance = 0
    const currentDistance = 0
    function calculateDistance(touches) {
      if (touches.length >= 2) {
        const x1 = touches[0].clientX
        const y1 = touches[0].clientY
        const x2 = touches[1].clientX
        const y2 = touches[1].clientY
        return Math.sqrt((x1 - x2) ** 2 + (y1 - y2) ** 2)
      }
      return 0
    }
    function handleTouchStart(event) {
      startX = event.touches[0].clientX
      startY = event.touches[0].clientY
      if (event.touches.length >= 2) {
        initialDistance = calculateDistance(event.touches)
      }
    }
    function handleTouchMove(event) {
      if (PDFViewerApplication.pdfCursorTools.active !== 0) {
        var xDiff = event.touches[0].clientX - startX
        var yDiff = event.touches[0].clientY - startY
        var scrollTop = viewerContainer.scrollTop - yDiff
        var scrollLeft = viewerContainer.scrollLeft - xDiff
        if (viewerContainer.scrollTo) {
          viewerContainer.scrollTo({ top: scrollTop, left: scrollLeft, behavior: 'instant' })
        }
      }
    }
    function handleTouchEnd(event) {
      endToucheX = event.changedTouches[0].clientX
      const deltaX = endToucheX - startX
      if (Math.abs(deltaX) > 50 && PDFViewerApplication.pdfCursorTools.active === 0) {
        if (deltaX > 0) {
          oTurn.turn('previous')
        } else {
          nextPage()
        }
      }
    }
    viewerContainer.addEventListener('touchstart', handleTouchStart, false)
    viewerContainer.addEventListener('touchend', handleTouchEnd, false)
    viewerContainer.addEventListener('touchmove', handleTouchMove, false)
    function nextPage() {
      var currnetPage = PDFViewerApplication.pdfViewer.currentPageNumber
      if (currnetPage === 1) {
        if (_toDoublePage) {
          bookFlip.spread(2)
          PDFViewerApplication.eventBus.dispatch('spreadmodechanged', {
            source: PDFViewerApplication,
            mode: 2
          })
        }
        if (isSinglePage()) {
          viewerContainer.classList.remove('singlePage')
          viewerContainer.classList.add('paper')
        }
        setTimeout(() => {
          oTurn.turn('next')
        }, 350)
      } else {
        oTurn.turn('next')
      }
    }
    $('#viewer').bind('start', function (event, pageObject, corner) {
      if (!navigator.maxTouchPoints) {
        if (corner == 'tl' || corner == 'tr' || corner == 'bl' || corner == 'br') {
          event.preventDefault()
        }
      }
    })
  },
  stop: function () {
    if (!this.active) return
    this.active = false
    var viewer = PDFViewerApplication.pdfViewer
    $('#viewer').turn('destroy')
    viewer.scrollPageIntoView = this._intoView
    viewer._getVisiblePages = this._visPages
    PDFViewerApplication.eventBus._listeners.switchspreadmode = this._evSpread
    viewer.spreadMode = this._spreadBk
    $('#viewer .page').removeAttr('style')
    $('#viewer').removeAttr('style').removeClass('shadow bookViewer').addClass('pdfViewer')
    var parent = this
    $('#viewer .page').each(function () {
      var page = $(this).attr('data-page-number')
      $(this).css('width', parent._size(page, 'width')).css('height', parent._size(page, 'height'))
    })
  },
  resize: function () {
    if (!this.active) return
    var page = PDFViewerApplication.page
    $('#viewer').turn(
      'size',
      this._size(page, 'width') * this._spreadMult(),
      this._size(page, 'height')
    )
  },
  rotate: function () {
    if (!this.active) return
    ;[this._height, this._width] = [this._width, this._height]
    this.resize()
  },
  spread: function (spreadMode) {
    if (!this.active) return
    this._spread = spreadMode
    $('#viewer').turn('display', this._spreadType())
    this.resize()
  },
  flip: function () {
    if (!this.active) return
    $('#viewer').turn('page', PDFViewerApplication.page)
    if (!PDFViewerApplication.pdfViewer.hasEqualPageSizes) this.resize()
  },
  link: function (data) {
    if (!this.active) return
    PDFViewerApplication.page = data.pageNumber
  },
  load: function () {
    if (!this.active) return
    var views = PDFViewerApplication.pdfViewer._pages
    var arr = []
    var page = PDFViewerApplication.page
    var min = Math.max(page - (this._spread === 0 ? 2 : 3 + (page % 2)), 0)
    var max = Math.min(page + (this._spread === 0 ? 1 : 3 - (page % 2)), views.length)
    for (var i = min, ii = max; i < ii; i++) {
      arr.push({ id: views[i].id, view: views[i], x: 0, y: 0, percent: 100 })
    }
    return { first: arr[page - min - 1], last: arr[arr.length - 1], views: arr }
  },
  _spreadType: function () {
    return this._spread === 0 ? 'single' : 'double'
  },
  _spreadMult: function () {
    return this._spread === 0 ? 1 : 2
  },
  _size: function (page, request) {
    var size
    if (request === 'width') size = this._width[page]
    if (request === 'height') size = this._height[page]
    return size * PDFViewerApplication.pdfViewer.currentScale
  }
}
bookFlip.init()
function switchSpreadMode(evt) {
  bookFlip.spread(evt.originalEvent.detail.mode)
  PDFViewerApplication.eventBus.dispatch('spreadmodechanged', {
    source: PDFViewerApplication,
    mode: evt.originalEvent.detail.mode
  })
}
function isSinglePage() {
  return $('#spreadNone').is(':visible')
}
